using YandexMobileAds;
using YandexMobileAds.Base;
using System;

namespace DZGames.Flags.Services
{
    public class YandexAdsService : IAdsService
    {
        public event Action RewardedAdViewed;
        
        private InterstitialAdLoader _interstitialAdLoader;
        private Interstitial _interstitial;
        private RewardedAdLoader _rewardedAdLoader;
        private RewardedAd _rewardedAd;

        private const string InterstitialID = "R-M-7345141-1";
        private const string RewardedAdID = "R-M-7345141-2";
        
        public void SetupLoader()
        {
            _interstitialAdLoader = new InterstitialAdLoader();
            _interstitialAdLoader.OnAdLoaded += HandleInterstitialLoaded;
            _interstitialAdLoader.OnAdFailedToLoad += HandleInterstitialFailedToLoad;
            
            _rewardedAdLoader = new RewardedAdLoader();
            _rewardedAdLoader.OnAdLoaded += HandleAdLoaded;
            _rewardedAdLoader.OnAdFailedToLoad += HandleAdFailedToLoad;
        }
        
        public void RequestInterstitial()
        {
            AdRequestConfiguration adRequestConfiguration = new AdRequestConfiguration.Builder(InterstitialID).Build();
            _interstitialAdLoader.LoadAd(adRequestConfiguration);
        }
        
        public void RequestRewardedAd()
        {
            AdRequestConfiguration adRequestConfiguration = new AdRequestConfiguration.Builder(RewardedAdID).Build();
            _rewardedAdLoader.LoadAd(adRequestConfiguration);
        }
        
        public void ShowInterstitial()
        {
            if (_interstitial != null)
                _interstitial.Show();
        }

        public void ShowRewardedAd()
        {
            if (_rewardedAd != null)
                _rewardedAd.Show();
        }

        #region Обработчики событий Interstitial
        private void HandleInterstitialLoaded(object sender, InterstitialAdLoadedEventArgs args)
        {
            _interstitial = args.Interstitial;
            
            _interstitial.OnAdClicked += HandleAdClicked;
            _interstitial.OnAdShown += HandleInterstitialShown;
            _interstitial.OnAdFailedToShow += HandleInterstitialFailedToShow;
            _interstitial.OnAdImpression += HandleImpression;
            _interstitial.OnAdDismissed += HandleInterstitialDismissed;
        }
        
        private void HandleInterstitialFailedToLoad(object sender, AdFailedToLoadEventArgs args)
        {
        }
        
        private void HandleInterstitialDismissed(object sender, EventArgs args)
        {
            DestroyInterstitial();
            RequestInterstitial();
        }
        
        private void HandleInterstitialFailedToShow(object sender, EventArgs args)
        {
            DestroyInterstitial();
            RequestInterstitial();
        }

        private void HandleAdClicked(object sender, EventArgs args)
        {
        }

        private void HandleInterstitialShown(object sender, EventArgs args)
        {
        }

        private void HandleImpression(object sender, ImpressionData impressionData)
        {
        }
        
        private void DestroyInterstitial()
        {
            if (_interstitial != null)
            {
                _interstitial.Destroy();
                _interstitial = null;
            }
        }
        #endregion

        #region Обработчики событий RewardedAd
        private void HandleAdLoaded(object sender, RewardedAdLoadedEventArgs args)
        {
            _rewardedAd = args.RewardedAd;
            
            _rewardedAd.OnAdClicked += HandleAdClicked;
            _rewardedAd.OnAdShown += HandleAdShown;
            _rewardedAd.OnAdFailedToShow += HandleAdFailedToShow;
            _rewardedAd.OnAdImpression += HandleImpression;
            _rewardedAd.OnAdDismissed += HandleAdDismissed;
            _rewardedAd.OnRewarded += HandleRewarded;
        }
        
        private void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
        {
        }

        private void HandleAdDismissed(object sender, EventArgs args)
        {
            DestroyRewardedAd();
            RequestRewardedAd();
        }
        
        private void HandleAdFailedToShow(object sender, AdFailureEventArgs args)
        {
            DestroyRewardedAd();
            RequestRewardedAd();
        }

        private void HandleAdShown(object sender, EventArgs args)
        {
        }

        private void HandleRewarded(object sender, Reward args) =>
            RewardedAdViewed?.Invoke();

        private void DestroyRewardedAd()
        {
            if (_rewardedAd != null)
            {
                _rewardedAd.Destroy();
                _rewardedAd = null;
            }
        }
        #endregion
    }
}