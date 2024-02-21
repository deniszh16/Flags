using Logic.WorldMap;
using Services.PersistentProgress;
using Services.StaticDataService;
using StaticData;

namespace Services.StateMachine.States
{
    public class MapState : BaseStates
    {
        private readonly IPersistentProgressService _progressService;
        private readonly IStaticDataService _staticData;
        
        private readonly Countries _countries;
        private readonly MapProgress _mapProgress;
        private readonly CurrentCountry _currentCountry;
        
        public MapState(GameStateMachine stateMachine, IPersistentProgressService progressService, IStaticDataService staticData,
            Countries countries, MapProgress mapProgress, CurrentCountry currentCountry) : base(stateMachine)
        {
            _progressService = progressService;
            _staticData = staticData;
            
            _countries = countries;
            _mapProgress = mapProgress;
            _currentCountry = currentCountry;
        }

        public override void Enter()
        {
            int progress = _progressService.GetUserProgress.Progress;
            LevelsStaticData levelsStaticData = _staticData.GetLevelConfig();
            
            _countries.Construct(progress, levelsStaticData);
            _countries.CheckCountries();
            _countries.MoveMapToCurrentCountry();
            _mapProgress.CalculatePassPercentage(progress, levelsStaticData.LevelConfig.Count);
            _currentCountry.ChangeTranslationKey(levelsStaticData.LevelConfig[progress - 1].LocalizedText);
        }

        public override void Exit()
        {
        }
    }
}