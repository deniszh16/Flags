using StaticData;

namespace Services.StaticDataService
{
    public interface IStaticDataService
    {
        public void LoadLevelsConfig();
        public LevelsStaticData GetLevelConfig();
    }
}