using UnityEngine;
using StaticData;

namespace Services.StaticDataService
{
    public class StaticDataService : IStaticDataService
    {
        private const string PathLevelsStaticData = "StaticData/LevelsStaticData";
        private LevelsStaticData _levelsStaticData;
        
        public void LoadLevelsConfig() =>
            _levelsStaticData = Resources.Load<LevelsStaticData>(PathLevelsStaticData);

        public LevelsStaticData GetLevelConfig() =>
            _levelsStaticData;
    }
}