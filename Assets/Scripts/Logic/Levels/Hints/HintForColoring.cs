using Logic.Levels.Coloring;
using Services.PersistentProgress;
using Services.SaveLoad;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Zenject;
using TMPro;

namespace Logic.Levels.Hints
{
    public class HintForColoring : MonoBehaviour
    {
        [Header("Ссылки на компоненты")]
        [SerializeField] private Button _hintButton;
        [SerializeField] private TextMeshProUGUI _numberOfHints;
        [SerializeField] private Button _closeWindowButton;

        [Header("Панель получения подсказок")]
        [SerializeField] private GameObject _gettingHints;
        
        private const float AnimationDuration = 0.3f;

        private IPersistentProgressService _progressService;
        private ISaveLoadService _saveLoadService;
        private ColoringFlag _coloringFlag;
        private ArrangementOfColors _arrangementOfColors;

        [Inject]
        private void Construct(IPersistentProgressService progressService, ISaveLoadService saveLoadService,
            ColoringFlag coloringFlag, ArrangementOfColors arrangementOfColors)
        {
            _progressService = progressService;
            _saveLoadService = saveLoadService;
            _coloringFlag = coloringFlag;
            _arrangementOfColors = arrangementOfColors;
        }

        public void ChangeActivityOfHintButton(bool state) =>
            _hintButton.interactable = state;

        public void ShowNumberOfHints() =>
            _numberOfHints.text = _progressService.GetUserProgress.Hints.ToString();

        private void OnEnable()
        {
            _hintButton.onClick.AddListener(UseHint);
            _closeWindowButton.onClick.AddListener(CloseHintWindow);
            _progressService.GetUserProgress.ChangedNumberOfHints += ShowNumberOfHints;
        }

        private void UseHint()
        {
            if (_progressService.GetUserProgress.Hints > 0 || _progressService.GetUserProgress.EndlessHints)
            {
                var fragmentAndColor = _arrangementOfColors.FindFragmentAndColorForHint();
                _coloringFlag.ColorInFragmentWithHint(fragmentAndColor.Item1, fragmentAndColor.Item2);
                _progressService.GetUserProgress.ChangeNumberOfHintsUsed();
                _progressService.GetUserProgress.ChangeNumberOfHints(-1);
                _saveLoadService.SaveProgress();
            }
            else
            {
                _gettingHints.SetActive(true);
                _gettingHints.transform.localScale = Vector3.zero;
                _gettingHints.transform.DOScale(Vector3.one, AnimationDuration);
            }
        }
        
        public void CloseHintWindow() =>
            _gettingHints.SetActive(false);

        private void OnDisable()
        {
            _hintButton.onClick.RemoveListener(UseHint);
            _closeWindowButton.onClick.RemoveListener(CloseHintWindow);
            _progressService.GetUserProgress.ChangedNumberOfHints -= ShowNumberOfHints;
        }
    }
}