using Services.PersistentProgress;
using Services.SaveLoad;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.UI;
using Zenject;

namespace Logic.Levels.Hints
{
    public class FreeHintButton : MonoBehaviour
    {
        [Header("Ссылки на компоненты")]
        [SerializeField] private Button _button;
        [SerializeField] private LocalizeStringEvent _localizeStringEvent;

        [Header("Текст об использовании")]
        [SerializeField] private LocalizedString _localizedString;
        
        private IPersistentProgressService _progressService;
        private ISaveLoadService _saveLoadService;

        [Inject]
        private void Construct(IPersistentProgressService progressService, ISaveLoadService saveLoadService)
        {
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        private void OnEnable()
        {
            if (_progressService.GetUserProgress.FreeHint == false)
            {
                _button.interactable = true;
                _button.onClick.AddListener(GetFreeHint);
            }
            else
            {
                _button.interactable = false;
                _localizeStringEvent.StringReference = _localizedString;
                _localizeStringEvent.RefreshString();
            }
        }

        private void GetFreeHint()
        {
            _button.interactable = false;
            _progressService.GetUserProgress.ChangeNumberOfHints(1);
            _progressService.GetUserProgress.ChangeFreeHints();
            _saveLoadService.SaveProgress();
        }

        private void OnDisable() =>
            _button.onClick.RemoveListener(GetFreeHint);
    }
}