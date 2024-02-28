using Services.PersistentProgress;
using Services.StaticDataService;
using Logic.Levels;
using Logic.Levels.Factory;

namespace Services.StateMachine.States
{
    public class DrawingState : BaseStates
    {
        private readonly IPersistentProgressService _progressService;
        private IStaticDataService _staticData;
        
        private IFlagFactory _flagFactory;
        private DrawingSection _drawingSection;
        private readonly DrawingRoute _drawingRoute;
        private readonly DescriptionTask _descriptionTask;
        private readonly ArrangementOfColors _arrangementOfColors;
        
        public DrawingState(GameStateMachine stateMachine, IPersistentProgressService progressService, IStaticDataService staticData, IFlagFactory flagFactory,
            DrawingSection drawingSection, DrawingRoute drawingRoute, DescriptionTask descriptionTask, ArrangementOfColors arrangementOfColors) : base(stateMachine)
        {
            _progressService = progressService;
            _staticData = staticData;
            
            _flagFactory = flagFactory;
            _drawingSection = drawingSection;
            _drawingRoute = drawingRoute;
            _descriptionTask = descriptionTask;
            _arrangementOfColors = arrangementOfColors;
        }

        public override void Enter()
        {
            _drawingSection.ChangeVisibilityOfDrawingSection(state: true);
            _drawingSection.CreateFlag(_flagFactory, _staticData.GetLevelConfig().LevelConfig[_progressService.GetUserProgress.Progress - 1].Flag);
            _flagFactory.FlagCreated += _drawingRoute.PrepareRouteForPencil;
            _drawingRoute.ChangeDrawingActivity(state: true);
            _drawingRoute.FragmentDrawn += _drawingSection.ShowDrawnLine;
            _descriptionTask.ChangeDescription(Description.Drawing);
            _drawingRoute.DrawingCompleted += GoToColoringState;
            _arrangementOfColors.ArrangeColors(_staticData.GetLevelConfig().LevelConfig[_progressService.GetUserProgress.Progress - 1].Colors);
        }

        private void GoToColoringState() =>
            _stateMachine.Enter<ColoringState>();

        public override void Exit()
        {
            _drawingRoute.ChangeDrawingActivity(state: false);
            _flagFactory.FlagCreated -= _drawingRoute.PrepareRouteForPencil;
            _drawingRoute.FragmentDrawn -= _drawingSection.ShowDrawnLine; 
            _drawingRoute.DrawingCompleted -= GoToColoringState;
        }
    }
}