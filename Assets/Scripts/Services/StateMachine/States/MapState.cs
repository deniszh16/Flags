﻿using Services.PersistentProgress;
using Services.StaticDataService;
using Logic.WorldMap;
using Logic.Buttons;
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
        private readonly LevelStartButton _levelStartButton;

        private bool _firstStart = true;
        
        public MapState(GameStateMachine stateMachine, IPersistentProgressService progressService, IStaticDataService staticData,
            Countries countries, MapProgress mapProgress, CurrentCountry currentCountry, LevelStartButton levelStartButton) : base(stateMachine)
        {
            _progressService = progressService;
            _staticData = staticData;
            
            _countries = countries;
            _mapProgress = mapProgress;
            _currentCountry = currentCountry;
            _levelStartButton = levelStartButton;
        }

        public override void Enter()
        {
            int progress = _progressService.GetUserProgress.Progress - 1;
            LevelsStaticData levelsStaticData = _staticData.GetLevelConfig();
            
            _countries.CheckCountries(progress);
            _countries.MoveMapToCurrentCountry(levelsStaticData, _firstStart);
            _mapProgress.CalculatePassPercentage(progress, numberOfTasks: levelsStaticData.LevelConfig.Count);
            _currentCountry.ChangeTranslationKey(levelsStaticData.LevelConfig[progress].LocalizedText);
            _levelStartButton.ShowButtonAnimation();
        }

        public override void Exit() =>
            _firstStart = false;
    }
}