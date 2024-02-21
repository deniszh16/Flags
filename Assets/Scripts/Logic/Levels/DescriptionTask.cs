using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;

namespace Logic.Levels
{
    public class DescriptionTask : MonoBehaviour
    {
        [Header("Ссылки на компоненты")]
        [SerializeField] private LocalizeStringEvent _localizeStringEvent;
        
        [Header("Описание задания")]
        [SerializeField] private LocalizedString[] _localizedStrings;

        public void ChangeDescription(Description description)
        {
            _localizeStringEvent.StringReference = _localizedStrings[(int)description];
            _localizeStringEvent.RefreshString();
        }
    }
}