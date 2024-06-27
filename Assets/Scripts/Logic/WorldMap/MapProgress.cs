using UnityEngine.Localization;
using UnityEngine;
using TMPro;

namespace Flags.Logic
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
        
        private void OnDisable() =>
            _localizedString.StringChanged -= UpdateText;

        public void CalculatePassPercentage(int progress, int numberOfTasks)
        {
            _progress = Mathf.Round((float)progress / numberOfTasks * 100);
            _localizedString.Arguments[0] = _progress;
            _localizedString.RefreshString();
        }

        private void UpdateText(string value) =>
            _textMeshPro.text = value;
    }
}