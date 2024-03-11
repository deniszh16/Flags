using Logic.Levels.Coloring;
using Logic.Levels.Factory;
using Logic.Levels.Other;
using Services.PersistentProgress;
using Services.SaveLoad;

namespace Services.StateMachine.States
{
    public class ResultsState : BaseStates
    {
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;
        
        private readonly GameResults _gameResults;
        private readonly IFlagFactory _flagFactory;
        private readonly LevelEffects _levelEffects;
        private readonly ColoringResult _coloringResult;
        
        public ResultsState(GameStateMachine stateMachine, IPersistentProgressService progressService, ISaveLoadService saveLoadService,
            GameResults gameResults, IFlagFactory flagFactory, LevelEffects levelEffects, ColoringResult coloringResult) : base(stateMachine)
        {
            _progressService = progressService;
            _saveLoadService = saveLoadService;
            
            _gameResults = gameResults;
            _flagFactory = flagFactory;
            _levelEffects = levelEffects;
            _coloringResult = coloringResult;
        }

        public override void Enter()
        {
            _gameResults.ChangeActivityOfResultsPanel(state: true);
            _gameResults.ChangeVisibilityOfDrawingSection(state: true);
            _gameResults.UpdateStatistics(_progressService.GetUserProgress);
            _levelEffects.ShowConfettiEffect();
            
            _flagFactory.RemovePreviousFlag();
            _coloringResult.HideResultIcon();
            
            _progressService.GetUserProgress.IncreaseProgress();
            _saveLoadService.SaveProgress();
        }

        public override void Exit()
        {
            _gameResults.ChangeVisibilityOfDrawingSection(state: false);
            _gameResults.ChangeActivityOfResultsPanel(state: false);
        }
    }
}