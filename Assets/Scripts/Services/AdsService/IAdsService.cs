using System;

namespace DZGames.Flags.Services
{
    public interface IAdsService
    {
        public event Action RewardedAdViewed;
        
        public void ShowInterstitial();
        public void ShowRewardedAd();
    }
}