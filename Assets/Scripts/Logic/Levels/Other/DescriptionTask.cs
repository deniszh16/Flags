using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;

namespace Logic.Levels.Other
{
    public class DescriptionTask : MonoBehaviour
    {
        [Header("Ссылки на компоненты")]
        [SerializeField] private LocalizeStringEvent _localizeStringEvent;
        
        [Header("Тексты задания")]
        [SerializeField] private LocalizedString[] _localizedStrings;

        public void ChangeDescription(DescriptionTypes description)
        {
            _localizeStringEvent.StringReference = _localizedStrings[(int)description];
            _localizeStringEvent.RefreshString();
        }
    }
}