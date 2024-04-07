using Services.StateMachine.States;
using Services.PersistentProgress;
using Services.StateMachine;
using Logic.Levels.Coloring;
using Logic.Levels.Factory;
using Services.SaveLoad;
using Logic.UI.Levels;

namespace Logic.Levels.StateMachine.States
{
    public class ResultsState : BaseStates
    {
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;

        private readonly IFlagFactory _flagFactory;
        
        private readonly ColoringResult _coloringResult;

        private readonly LevelEffects _levelEffects;
        private readonly GameResults _gameResults;

        public ResultsState(GameStateMachine stateMachine, IPersistentProgressService progressService, ISaveLoadService saveLoadService,
            IFlagFactory flagFactory, ColoringResult coloringResult, LevelEffects levelEffects, GameResults gameResults) : base(stateMachine)
        {
            _progressService = progressService;
            _saveLoadService = saveLoadService;

            _flagFactory = flagFactory;

            _coloringResult = coloringResult;

            _levelEffects = levelEffects;
            _gameResults = gameResults;
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