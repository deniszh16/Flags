using DZGames.Flags.StaticData;

namespace DZGames.Flags.Services
{
    public interface IStaticDataService
    {
        public void LoadLevelsConfig();
        public LevelsStaticData GetLevelConfig();
    }
}