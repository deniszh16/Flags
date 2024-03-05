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

        [Header("Дополнительные подсказки")]
        [SerializeField] private GameObject _gettingHints;

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
            _hintButton.onClick.AddListener(ShowNumberOfHints);
            _closeWindowButton.onClick.AddListener(CloseHintWindow);
            _progressService.GetUserProgress.ChangedNumberOfHints += ShowNumberOfHints;
        }

        private void UseHint()
        {
            if (_progressService.GetUserProgress.Hints > 0 || _progressService.GetUserProgress.EndlessHints)
            {
                (int, Color) fragment = _arrangementOfColors.FindFragmentForHint();
                _coloringFlag.ColorInFragmentWithHint(fragment.Item1, fragment.Item2);
                _progressService.GetUserProgress.ChangeNumberOfHints(-1);
                _saveLoadService.SaveProgress();
            }
            else
            {
                _gettingHints.SetActive(true);
                _gettingHints.transform.localScale = Vector3.zero;
                _gettingHints.transform.DOScale(Vector3.one, duration: 0.3f);
            }
        }
        
        public void CloseHintWindow() =>
            _gettingHints.SetActive(false);

        private void OnDisable()
        {
            _hintButton.onClick.RemoveListener(UseHint);
            _hintButton.onClick.RemoveListener(ShowNumberOfHints);
            _closeWindowButton.onClick.RemoveListener(CloseHintWindow);
            _progressService.GetUserProgress.ChangedNumberOfHints -= ShowNumberOfHints;
        }
    }
}