using Services.PersistentProgress;
using Services.StaticDataService;
using Logic.Levels.Coloring;
using Logic.Levels.Drawing;
using Logic.Levels.Factory;
using Logic.Levels.Hints;
using Logic.Levels.Other;
using UniRx;

namespace Services.StateMachine.States
{
    public class DrawingState : BaseStates
    {
        private readonly IPersistentProgressService _progressService;
        private readonly IStaticDataService _staticData;
        
        private readonly IFlagFactory _flagFactory;
        private readonly DrawingSection _drawingSection;
        private readonly DrawingRoute _drawingRoute;
        private readonly DescriptionTask _descriptionTask;
        private readonly InfoCurrentLevel _infoCurrentLevel;
        private readonly ArrangementOfColors _arrangementOfColors;
        private readonly HintForColoring _hintForColoring;

        private readonly CompositeDisposable _compositeDisposable = new();
        
        public DrawingState(GameStateMachine stateMachine, IPersistentProgressService progressService, IStaticDataService staticData,
            IFlagFactory flagFactory, DrawingSection drawingSection, DrawingRoute drawingRoute, DescriptionTask descriptionTask,
            InfoCurrentLevel infoCurrentLevel, ArrangementOfColors arrangementOfColors, HintForColoring hintForColoring) : base(stateMachine)
        {
            _progressService = progressService;
            _staticData = staticData;
            
            _flagFactory = flagFactory;
            _drawingSection = drawingSection;
            _drawingRoute = drawingRoute;
            _descriptionTask = descriptionTask;
            _infoCurrentLevel = infoCurrentLevel;
            _arrangementOfColors = arrangementOfColors;
            _hintForColoring = hintForColoring;
        }

        public override void Enter()
        {
            _drawingSection.ChangeVisibilityOfDrawingSection(state: true);
            _drawingSection.CreateFlag(_flagFactory, _staticData.GetLevelConfig().LevelConfig[_progressService.GetUserProgress.Progress - 1].Flag);
            _flagFactory.FlagCreated.Subscribe(_drawingRoute.PrepareRouteForPencil).AddTo(_compositeDisposable);
            _drawingRoute.ChangeDrawingActivity(state: true);
            _drawingRoute.FragmentDrawn.Subscribe(_ => _flagFactory.GetCreatedFlag.ShowDrawnLines()).AddTo(_compositeDisposable);
            _drawingRoute.DrawingCompleted.Subscribe(_ => _stateMachine.Enter<ColoringState>()).AddTo(_compositeDisposable);
            _descriptionTask.ChangeDescription(DescriptionTypes.Drawing);
            _infoCurrentLevel.ShowCurrentLevel(_progressService.GetUserProgress.Progress);
            _infoCurrentLevel.ShowCountryName(_staticData.GetLevelConfig().LevelConfig[_progressService.GetUserProgress.Progress - 1].LocalizedText);
            _arrangementOfColors.ChangeVisibilityOfColors(state: true);
            _arrangementOfColors.ArrangeColors(_staticData.GetLevelConfig().LevelConfig[_progressService.GetUserProgress.Progress - 1].Colors);
            _hintForColoring.ShowNumberOfHints();
        }

        public override void Exit()
        {
            _compositeDisposable.Clear();
            _drawingRoute.ChangeDrawingActivity(state: false);
        }
    }
}