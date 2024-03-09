using UnityEngine.Localization;
using UnityEngine;
using TMPro;

namespace Logic.WorldMap
{
    public class MapProgress : MonoBehaviour
    {
        [Header("Ссылки на компоненты")]
        [SerializeField] private TextMeshProUGUI _textMeshPro;
        [SerializeField] private LocalizedString _localizedString;

        private float _progress;

        private void OnEnable()
        {
            _localizedString.Arguments = new object[] { _progress };
            _localizedString.StringChanged += UpdateText;
        }

        private void UpdateText(string value) =>
            _textMeshPro.text = value;

        public void CalculatePassPercentage(int progress, int numberOfTasks)
        {
            _progress = (float)progress / numberOfTasks * 100;
            _localizedString.Arguments[0] = _progress;
            _localizedString.RefreshString();
        }

        private void OnDisable() =>
            _localizedString.StringChanged -= UpdateText;
    }
}