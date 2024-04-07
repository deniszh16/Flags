using Services.PersistentProgress;
using Cysharp.Threading.Tasks;
using Services.Localization;
using Services.SceneLoader;
using Services.SaveLoad;
using Services.Sound;
using UnityEngine;
using Zenject;
using Data;

namespace Bootstraper
{
    public class GameBootstraper : MonoBehaviour
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
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            Application.targetFrameRate = 60;
        }
        
        private void Start()
        {
            LoadProgressOrInitNew();
            _localizationService.SetLocale(_progressService.GetUserProgress.SettingsData.Locale);
            _soundService.SoundActivity = _progressService.GetUserProgress.SettingsData.Sound;
            _sceneLoaderService.LoadSceneAsync(Scenes.MainMenu, screensaver: false, delay: 1f).Forget();
        }
        
        private void LoadProgressOrInitNew()
        {
            _progressService.GetUserProgress =
                _saveLoadService.LoadProgress() ?? new UserProgress();
        }
    }
}