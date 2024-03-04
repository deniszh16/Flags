using Logic.Levels.Coloring;
using Services.PersistentProgress;
using Services.SaveLoad;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using TMPro;

namespace Logic.Levels.Hints
{
    public class HintForColoring : MonoBehaviour
    {
        [Header("Ссылки на компоненты")]
        [SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI _numberOfHints;

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
            _button.interactable = state;

        public void ShowNumberOfHints() =>
            _numberOfHints.text = _progressService.GetUserProgress.Hints.ToString();

        private void OnEnable()
        {
            _button.onClick.AddListener(UseHint);
            _button.onClick.AddListener(ShowNumberOfHints);
        }

        private void UseHint()
        {
            if (_progressService.GetUserProgress.Hints > 0)
            {
                (int, Color) fragment = _arrangementOfColors.FindFragmentForHint();
                _coloringFlag.ColorInFragmentWithHint(fragment.Item1, fragment.Item2);
                _progressService.GetUserProgress.ChangeNumberOfHints(-1);
                _saveLoadService.SaveProgress();
            }
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(UseHint);
            _button.onClick.RemoveListener(ShowNumberOfHints);
        }
    }
}