using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Logic.Levels
{
    public class ColoringFlag : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [Header("Иконка кисти")]
        [SerializeField] private Transform _brush;
        [SerializeField] private Image _whiteBrush;

        private Color? _activeColor;
        private bool _coloringActivity;
        private bool _tappingScreen;

        private Flag _currentFlag;
        private int _currentFragment;
        private Image _currentFragmentImage;
        
        private const float _fillStep = 0.1f;
        private const float _fillingSpeed = 25f;
        
        public event Action StartedColoring;
        public event Action FinishedColoring;
        public event Action FlagIsFinished;
        private bool _startedColoring;

        public void SetFlag(Flag flag) =>
            _currentFlag = flag;

        public void ChangeColoringActivity(bool state) =>
            _coloringActivity = state;

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
        }

        public void OnPointerUp(PointerEventData eventData) =>
            _tappingScreen = false;
        
        private void Update()
        {
            if (_coloringActivity == false || _tappingScreen == false)
                return;
            
            if (_activeColor.HasValue)
            {
                if (_startedColoring == false)
                {
                    _startedColoring = true;
                    StartedColoring?.Invoke();
                }
                
                if (_currentFragmentImage != null & _currentFragmentImage.fillAmount < 1)
                {
                    _currentFragmentImage.color = _activeColor.Value;
                    _currentFragmentImage.fillAmount += _fillStep * _fillingSpeed * Time.deltaTime;
                }
                else
                {
                    _currentFlag.DeselectFragment(_currentFragment);
                    _brush.gameObject.SetActive(false);
                    _activeColor = default;
                    _currentFragment++;
                    FinishedColoring?.Invoke();
                    _startedColoring = false;
                    CheckCompletionOfColoring();
                }
            }
        }

        private void CheckCompletionOfColoring()
        {
            if (_currentFragment < _currentFlag.NumberOfEmptyFragments)
                GetCurrentFragment();
            else FlagIsFinished?.Invoke();
        }

        private void OnDestroy()
        {
            StartedColoring = null;
            FinishedColoring = null;
            FlagIsFinished = null;
        }
    }
}