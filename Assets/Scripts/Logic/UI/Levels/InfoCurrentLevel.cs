using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using TMPro;

namespace DZGames.Flags.Logic
{
    public class InfoCurrentLevel : MonoBehaviour
    {
        [Header("Текущий уровень")]
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private LocalizedString _levelLocalizedString; 
        
        [Header("Текущая страна")]
        [SerializeField] private LocalizeStringEvent _countryLocalizedString;
        
        private int _level;
        
        private void OnEnable()
        {
            _levelLocalizedString.Arguments = new object[] { _level };
            _levelLocalizedString.StringChanged += UpdateText;
        }
        
        private void OnDisable() =>
            _levelLocalizedString.StringChanged -= UpdateText;
        
        public void ShowCurrentLevel(int progress)
        {
            _level = progress;
            _levelLocalizedString.Arguments[0] = _level;
            _levelLocalizedString.RefreshString();
        }
        
        public void ShowCountryName(LocalizedString localizedString)
        {
            _countryLocalizedString.StringReference = localizedString;
            _countryLocalizedString.RefreshString();
        }
        
        private void UpdateText(string value) =>
            _levelText.text = value;
    }
}