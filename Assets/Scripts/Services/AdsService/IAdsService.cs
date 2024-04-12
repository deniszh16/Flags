using System;

namespace Services.AdsService
{
    public interface IAdsService
    {
        public event Action RewardedAdViewed;
        
        public void ShowInterstitial();
        public void ShowRewardedAd();
    }
}