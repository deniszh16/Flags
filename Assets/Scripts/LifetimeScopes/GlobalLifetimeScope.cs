using DZGames.Flags.Services;
using VContainer.Unity;
using UnityEngine;
using VContainer;

namespace DZGames.Flags.LifetimeScopes
{
    public class GlobalLifetimeScope : LifetimeScope
    {
        [SerializeField] private SceneLoaderService _sceneLoaderService;
        [SerializeField] private SoundService _soundService;
        
        protected override void Configure(IContainerBuilder builder)
        {
            BindStaticData(builder);
            BindPersistentProgress(builder);
            BindSaveLoadService(builder);
            BindLocalizationService(builder);
            BindSceneLoader(builder);
            BindSoundService(builder);
            BindYandexAdsService(builder);
            DontDestroyOnLoad(gameObject);
        }

        private void BindStaticData(IContainerBuilder builder)
        {
            IStaticDataService staticDataService = new StaticDataService();
            staticDataService.LoadLevelsConfig();
            builder.RegisterInstance(staticDataService);
        }

        private void BindPersistentProgress(IContainerBuilder builder) =>
            builder.Register<PersistentProgressService>(Lifetime.Singleton).AsImplementedInterfaces();

        private void BindSaveLoadService(IContainerBuilder builder) =>
            builder.Register<SaveLoadService>(Lifetime.Singleton).AsImplementedInterfaces();

        private void BindLocalizationService(IContainerBuilder builder) =>
            builder.Register<LocalizationService>(Lifetime.Singleton).AsImplementedInterfaces();

        private void BindSceneLoader(IContainerBuilder builder) =>
            builder.RegisterComponent(_sceneLoaderService).AsImplementedInterfaces();

        private void BindSoundService(IContainerBuilder builder) =>
            builder.RegisterComponent(_soundService).AsImplementedInterfaces();

        private void BindYandexAdsService(IContainerBuilder builder)
        {
            YandexAdsService yandexAdsService = new YandexAdsService();
            yandexAdsService.SetupLoader();
            yandexAdsService.RequestInterstitial();
            yandexAdsService.RequestRewardedAd();
            builder.RegisterComponent(yandexAdsService).AsImplementedInterfaces();
        }
    }
}