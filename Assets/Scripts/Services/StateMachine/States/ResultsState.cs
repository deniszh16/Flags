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
        
        public ResultsState(GameStateMachine stateMachine, IPersistentProgressService progressService, ISaveLoadService saveLoadService,
            GameResults gameResults) : base(stateMachine)
        {
            _progressService = progressService;
            _saveLoadService = saveLoadService;
            
            _gameResults = gameResults;
        }

        public override void Enter()
        {
            _gameResults.ChangeActivityOfResultsPanel(state: true);
            _gameResults.ChangeVisibilityOfDrawingSection(state: true);
            _gameResults.UpdateStatistics(_progressService.GetUserProgress);
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