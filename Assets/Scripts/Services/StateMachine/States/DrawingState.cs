using Services.PersistentProgress;
using Services.StaticDataService;
using Logic.Levels;
using UnityEngine;

namespace Services.StateMachine.States
{
    public class DrawingState : BaseStates
    {
        private readonly IPersistentProgressService _progressService;
        private IStaticDataService _staticData;
        
        private readonly CurrentLevel _currentLevel;
        private readonly DescriptionTask _descriptionTask;
        private readonly DrawingRoute _drawingRoute;
        
        public DrawingState(GameStateMachine stateMachine, IPersistentProgressService progressService, IStaticDataService staticData,
            CurrentLevel currentLevel, DescriptionTask descriptionTask, DrawingRoute drawingRoute) : base(stateMachine)
        {
            _progressService = progressService;
            _staticData = staticData;
            
            _currentLevel = currentLevel;
            _descriptionTask = descriptionTask;
            _drawingRoute = drawingRoute;
        }

        public override void Enter()
        {
            _currentLevel.ShowDrawingSection();
            _currentLevel.CreateFlag(_staticData.GetLevelConfig().LevelConfig[_progressService.GetUserProgress.Progress - 1].Flag, _drawingRoute);
            _currentLevel.ArrangeColors(_staticData.GetLevelConfig().LevelConfig[_progressService.GetUserProgress.Progress - 1].Colors);
            _currentLevel.FlagCreated += ChangeDrawingActivity;
            _descriptionTask.ChangeDescription(Description.Drawing);
        }

        private void ChangeDrawingActivity() =>
            _drawingRoute.ChangeDrawingActivity(state: true);

        public override void Exit() =>
            _currentLevel.FlagCreated -= ChangeDrawingActivity;
    }
}