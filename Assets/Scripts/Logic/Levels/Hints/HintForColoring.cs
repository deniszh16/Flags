using Services.PersistentProgress;
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

        private IPersistentProgressService _progressService;

        [Inject]
        private void Construct(IPersistentProgressService progressService) =>
            _progressService = progressService;

        public void CheckNumberOfHints()
        {
            bool availableHints = _progressService.GetUserProgress.Hints > 0;
            ChangeActivityOfHintButton(state: availableHints);
            _numberOfHints.text = _progressService.GetUserProgress.Hints.ToString();
        }

        private void ChangeActivityOfHintButton(bool state) =>
            _button.interactable = state;
    }
}