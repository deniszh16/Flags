using Logic.Levels.Factory;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UniRx;

namespace Logic.Levels.Coloring
{
    public class ColoringFlag : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [Header("Кисть для раскрашивания")]
        [SerializeField] private Transform _brush;
        [SerializeField] private Image _whiteBrush;
        [SerializeField] private Animator _animator;

        private readonly int _animationTrigger = Animator.StringToHash(name: "Coloring");

        private Color? _activeColor;
        
        private bool _startedColoring;
        private bool _coloringActivity;
        private bool _tappingScreen;

        private Flag _currentFlag;
        private int _currentFragment;
        private Image _currentFragmentImage;

        private const float FillStep = 0.1f;
        private const float FillingSpeed = 25f;

        public readonly ReactiveCommand StartedColoring = new();
        public readonly ReactiveCommand FragmentIsColored = new();
        public readonly ReactiveCommand FlagIsFinished = new();

        public void ChangeColoringActivity(bool state) =>
            _coloringActivity = state;

        public void SetFlag(Flag flag)
        {
            _currentFlag = flag;
            _currentFragment = 0;
        }

        public void GetCurrentFragment() =>
            _currentFragmentImage = _currentFlag.SelectActiveFragment(_currentFragment);

        public void SetActiveColor(Color color) =>
            _activeColor = color;

        public void CustomizeBrush()
        {
            _brush.gameObject.SetActive(true);
            _brush.position = _currentFlag.GetPositionOfActiveFragment(_currentFragment);
            if (_activeColor != null) _whiteBrush.color = _activeColor.Value;
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            _tappingScreen = true;
            _animator.SetBool(id: _animationTrigger, value: true);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _tappingScreen = false;
            _animator.SetBool(id: _animationTrigger, value: false);
        }

        private void Update()
        {
            if (_coloringActivity == false || _tappingScreen == false)
                return;
            
            if (_activeColor.HasValue)
            {
                if (_startedColoring == false)
                {
                    _startedColoring = true;
                    StartedColoring.Execute();
                }
                
                if (_currentFragmentImage != null & _currentFragmentImage.fillAmount < 1)
                {
                    _currentFragmentImage.color = _activeColor.Value;
                    _currentFragmentImage.fillAmount += FillStep * FillingSpeed * Time.deltaTime;
                }
                else
                {
                    FinishColoringFragment();
                }
            }
        }
        
        private void FinishColoringFragment()
        {
            _currentFlag.DeselectFragment(_currentFragment);
            _brush.gameObject.SetActive(false);
            _activeColor = default;
            _currentFragment++;
            _startedColoring = false;
            
            FragmentIsColored.Execute();
            CheckCompletionOfColoring();
        }

        private void CheckCompletionOfColoring()
        {
            if (_currentFragment < _currentFlag.NumberOfEmptyFragments)
            {
                GetCurrentFragment();
            }
            else
            {
                _currentFlag = null;
                _currentFragmentImage = null;
                FlagIsFinished.Execute();
            }
        }

        public void ColorInFragmentWithHint(int fragment, Color color)
        {
            _currentFragment = fragment;
            GetCurrentFragment();
            
            _currentFragmentImage.color = color;
            _currentFragmentImage.fillAmount = 1;

            for (int i = fragment + 1; i < _currentFlag.NumberOfEmptyFragments; i++)
            {
                _currentFlag.DeselectFragment(i);
                _currentFlag.ResetFillOfLastFragment(i);
            }
            
            FinishColoringFragment();
        }
        
        public void ResetLastFragment()
        {
            if (_currentFragment > 0)
            {
                _currentFlag.DeselectFragment(_currentFragment);
                _currentFragment--;
                _currentFlag.ResetFillOfLastFragment(_currentFragment);
                _currentFragmentImage = null;
                _brush.gameObject.SetActive(false);
                GetCurrentFragment();
            }
        }

        public void ResetAllFragments()
        {
            _currentFragment = 0;
            _currentFragmentImage = null;
            _currentFlag.ResetFillColors();
            GetCurrentFragment();
        }
    }
}