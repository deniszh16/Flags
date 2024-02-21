using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;

namespace Logic.WorldMap
{
    public class CurrentCountry : MonoBehaviour
    {
        [Header("Ссылки на компоненты")]
        [SerializeField] private LocalizeStringEvent _localizeStringEvent;
        
        public void ChangeTranslationKey(LocalizedString localizedString)
        {
            _localizeStringEvent.StringReference = localizedString;
            _localizeStringEvent.RefreshString();
        }
    }
}