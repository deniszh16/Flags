using Cysharp.Threading.Tasks;
using DZGames.Flags.Services;
using UnityEngine;
using UniRx;

namespace DZGames.Flags.Logic
{
    public class DrawingState : BaseStates
    {
        private readonly IPersistentProgressService _progressService;
        private readonly IStaticDataService _staticData;
        private readonly IFlagFactory _flagFactory;
        
        private readonly DrawingSection _drawingSection;
        private readonly DrawingRoute _drawingRoute;
        
        private readonly ArrangementOfColors _arrangementOfColors;
        private readonly HintForColoring _hintForColoring;

        private readonly Tutorial _tutorial;
        private readonly InfoCurrentLevel _infoCurrentLevel;
        private readonly DescriptionTask _descriptionTask;
        private readonly LevelEffects _levelEffects;

        private readonly CompositeDisposable _compositeDisposable = new();
        
        public DrawingState(GameStateMachine stateMachine, IPersistentProgressService progressService, IStaticDataService staticData, IFlagFactory flagFactory,
            DrawingSection drawingSection, DrawingRoute drawingRoute, ArrangementOfColors arrangementOfColors, HintForColoring hintForColoring, Tutorial tutorial,
            InfoCurrentLevel infoCurrentLevel, DescriptionTask descriptionTask, LevelEffects levelEffects) : base(stateMachine)
        {
            _progressService = progressService;
            _staticData = staticData;
            _flagFactory = flagFactory;
            
            _drawingSection = drawingSection;
            _drawingRoute = drawingRoute;
            
            _arrangementOfColors = arrangementOfColors;
            _hintForColoring = hintForColoring;

            _tutorial = tutorial;
            _infoCurrentLevel = infoCurrentLevel;
            _descriptionTask = descriptionTask;
            _levelEffects = levelEffects;
        }

        public override void Enter()
        {
            _drawingSection.ChangeVisibilityOfDrawingSection(state: true);
            _drawingSection.ChangeVisibilityOfLoadingIcon(state: true);
            _drawingSection.CreateFlag(_flagFactory, _staticData.GetLevelConfig().LevelConfig[_progressService.GetUserProgress.Progress - 1].Flag);
            
            _flagFactory.FlagCreated.Subscribe(_drawingRoute.PrepareRouteForPencil).AddTo(_compositeDisposable);
            _flagFactory.FlagCreated.Subscribe(_ => _drawingSection.ChangeVisibilityOfLoadingIcon(state: false)).AddTo(_compositeDisposable);
            
            ShowTutorial();
            
            _drawingRoute.StartDrawing.Subscribe(_ => _tutorial.ChangeVisibilityOfTutorial(state: false)).AddTo(_compositeDisposable);
            _drawingRoute.ChangeDrawingActivity(state: true);
            _drawingRoute.FragmentDrawn.Subscribe(_ => _flagFactory.GetCreatedFlag.ShowDrawnLines()).AddTo(_compositeDisposable);
            _drawingRoute.DrawingCompleted.Subscribe(_ => _levelEffects.ShowDrawingFinishEffect()).AddTo(_compositeDisposable);
            _drawingRoute.DrawingCompleted.Subscribe(_ => _stateMachine.Enter<ColoringState>()).AddTo(_compositeDisposable);
            
            _descriptionTask.ChangeDescription(DescriptionTypes.Drawing);
            _infoCurrentLevel.ShowCurrentLevel(_progressService.GetUserProgress.Progress);
            _infoCurrentLevel.ShowCountryName(_staticData.GetLevelConfig().LevelConfig[_progressService.GetUserProgress.Progress - 1].LocalizedText);
            _arrangementOfColors.ChangeVisibilityOfColors(state: true);

            bool randomArrangement = _progressService.GetUserProgress.Progress > 1;
            _arrangementOfColors.ArrangeColors(_staticData.GetLevelConfig().LevelConfig[_progressService.GetUserProgress.Progress - 1].Colors, randomArrangement);
            
            _hintForColoring.ShowNumberOfHints();
        }
        
        public override void Exit()
        {
            _compositeDisposable.Clear();
            _drawingRoute.ChangeDrawingActivity(state: false);
            _tutorial.ChangeVisibilityOfTutorial(state: false);
        }

        private void ShowTutorial()
        {
            _flagFactory.FlagCreated.Subscribe(_ =>
            {
                if (_progressService.GetUserProgress.Progress <= 1)
                    _tutorial.ShowTutorial(delay: 0.2f, position: new Vector2(0, -150f)).Forget();
            }).AddTo(_compositeDisposable);
        }
    }
}