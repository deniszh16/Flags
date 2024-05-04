using DZGames.Flags.Services;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.UI;
using VContainer;

namespace DZGames.Flags.Logic
{
    public class FreeHintButton : MonoBehaviour
    {
        [Header("Ссылки на компоненты")]
        [SerializeField] private Button _button;
        [SerializeField] private LocalizeStringEvent _localizeStringEvent;

        [Header("Текст об использовании")]
        [SerializeField] private LocalizedString _localizedString;
        
        [Header("Эффект получения")]
        [SerializeField] private ParticleSystem _effect;
        
        private IPersistentProgressService _progressService;
        private ISaveLoadService _saveLoadService;

        [Inject]
        private void Construct(IPersistentProgressService progressService, ISaveLoadService saveLoadService)
        {
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }
        
        private void OnDisable() =>
            _button.onClick.RemoveListener(GetFreeHint);

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
            ShowHintEffect();
        }
        
        private void ShowHintEffect()
        {
            _effect.gameObject.SetActive(true);
            _effect.Play();
        }
    }
}