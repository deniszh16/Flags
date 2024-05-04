using System.Threading;
using Cysharp.Threading.Tasks;
using DZGames.Flags.Services;
using DZGames.Flags.Data;
using UnityEngine;
using VContainer;

namespace DZGames.Flags.Bootstrapper
{
    public class GameBootstrapper : MonoBehaviour
    {
        private IPersistentProgressService _progressService;
        private ISaveLoadService _saveLoadService;
        private ISceneLoaderService _sceneLoaderService;
        private ILocalizationService _localizationService;
        private ISoundService _soundService;

        [Inject]
        private void Construct(IPersistentProgressService progressService, ISaveLoadService saveLoadService,
            ISceneLoaderService sceneLoaderService, ILocalizationService localizationService, ISoundService soundService)
        {
            _progressService = progressService;
            _saveLoadService = saveLoadService;
            _sceneLoaderService = sceneLoaderService;
            _localizationService = localizationService;
            _soundService = soundService;
        }
        
        private void Awake()
        {
            Application.targetFrameRate = 60;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }
        
        private void Start()
        {
            LoadProgressOrInitNew();
            _localizationService.SetLocale(_progressService.GetUserProgress.SettingsData.Locale);
            _soundService.ChangeSoundActivity(_progressService.GetUserProgress.SettingsData.Sound);
            _sceneLoaderService.LoadSceneAsync(Scenes.MainMenu, screensaver: false, delay: 1f, CancellationToken.None).Forget();
        }
        
        private void LoadProgressOrInitNew() =>
            _progressService.SetUserProgress(_saveLoadService.LoadProgress() ?? new UserProgress());
    }
}