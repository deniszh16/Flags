using Flags.StaticData;

namespace Flags.Services
{
    public interface IStaticDataService
    {
        public void LoadLevelsConfig();
        public LevelsStaticData GetLevelConfig();
    }
}