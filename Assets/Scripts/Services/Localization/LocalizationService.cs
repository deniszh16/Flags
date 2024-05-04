using UnityEngine.Localization.Settings;
using Cysharp.Threading.Tasks;

namespace DZGames.Flags.Services
{
    public class LocalizationService : ILocalizationService
    {
        private bool _active;
        
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;
        
        private LocalizationService(IPersistentProgressService progressService, ISaveLoadService saveLoadService)
        {
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        public void SetLocale(int localeID) =>
            ChangeLocaleCoroutine(localeID).Forget();

        public void ChangeLocale()
        {
            if (_active) return;
            
            int localeID = _progressService.GetUserProgress.SettingsData.Locale == 0 ? 1 : 0;
            ChangeLocaleCoroutine(localeID).Forget();
        }

        private async UniTask ChangeLocaleCoroutine(int localeID)
        {
            _active = true;
            await LocalizationSettings.InitializationOperation;
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeID];
            _progressService.GetUserProgress.SettingsData.Locale = localeID;
            _saveLoadService.SaveProgress();
            _active = false;
        }
    }
}