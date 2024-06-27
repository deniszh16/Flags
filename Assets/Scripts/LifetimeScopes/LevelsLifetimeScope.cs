using Flags.Services;
using Flags.Logic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Flags.LifetimeScopes
{
    public class LevelsLifetimeScope : LifetimeScope
    {
        [Header("Компонент обновления")]
        [SerializeField] private MonoUpdateService _monoUpdateService;

        [Header("Компоненты карты")]
        [SerializeField] private Countries _countries;
        [SerializeField] private MapProgress _mapProgress;
        [SerializeField] private LevelStartButton _levelStartButton;
        [SerializeField] private CurrentCountry _currentCountry;

        [Header("Компоненты рисования")]
        [SerializeField] private DrawingSection _drawingSection;
        [SerializeField] private DrawingRoute _drawingRoute;

        [Header("Компоненты раскрашивания")]
        [SerializeField] private ArrangementOfColors _arrangementOfColors;
        [SerializeField] private ColoringFlag _coloringFlag;
        [SerializeField] private HintForColoring _hintForColoring;
        [SerializeField] private ColorCancellation _colorCancellation;
        [SerializeField] private ColoringResult _coloringResult;

        [Header("Компоненты викторины")]
        [SerializeField] private GuessingCapitals _guessingCapitals;

        [Header("Прочие компоненты")]
        [SerializeField] private Tutorial _tutorial;
        [SerializeField] private InfoCurrentLevel _levelInfo;
        [SerializeField] private DescriptionTask _descriptionTask;
        [SerializeField] private LevelEffects _levelEffects;
        [SerializeField] private GameResults _gameResults;

        protected override void Configure(IContainerBuilder builder)
        {
            BindGameStateMachine(builder);
            BindMonoUpdateService(builder);
            BindFlagFactory(builder);

            BindCountries(builder);
            BindMapProgress(builder);
            BindLevelStartButton(builder);
            BindCurrentCountry(builder);

            BindDrawingSection(builder);
            BindDrawingRoute(builder);
            
            BindArrangementOfButtonsColor(builder);
            BindColoringFlag(builder);
            BindHintForColoring(builder);
            BindColorCancellation(builder);
            BindColoringResult(builder);
            
            BindGuessingCapitals(builder);
            
            BindTutorial(builder);
            BindLevelInfo(builder);
            BindDescriptionTask(builder);
            BindLevelEffects(builder);
            BindGameResults(builder);
        }

        private void BindGameStateMachine(IContainerBuilder builder) =>
            builder.Register<GameStateMachine>(Lifetime.Singleton);

        private void BindMonoUpdateService(IContainerBuilder builder) =>
            builder.RegisterComponent(_monoUpdateService).AsImplementedInterfaces();

        private void BindFlagFactory(IContainerBuilder builder) =>
            builder.Register<FlagFactory>(Lifetime.Singleton).As<IFlagFactory>();

        private void BindCountries(IContainerBuilder builder) =>
            builder.RegisterComponent(_countries);

        private void BindMapProgress(IContainerBuilder builder) =>
            builder.RegisterComponent(_mapProgress);

        private void BindLevelStartButton(IContainerBuilder builder) =>
            builder.RegisterComponent(_levelStartButton);

        private void BindCurrentCountry(IContainerBuilder builder) =>
            builder.RegisterComponent(_currentCountry);

        private void BindDrawingSection(IContainerBuilder builder) =>
            builder.RegisterComponent(_drawingSection);

        private void BindDrawingRoute(IContainerBuilder builder) =>
            builder.RegisterComponent(_drawingRoute);
        
        private void BindArrangementOfButtonsColor(IContainerBuilder builder) =>
            builder.RegisterComponent(_arrangementOfColors);
        
        private void BindColoringFlag(IContainerBuilder builder) =>
            builder.RegisterComponent(_coloringFlag);
        
        private void BindHintForColoring(IContainerBuilder builder) =>
            builder.RegisterComponent(_hintForColoring);
        
        private void BindColorCancellation(IContainerBuilder builder) =>
            builder.RegisterComponent(_colorCancellation);
        
        private void BindColoringResult(IContainerBuilder builder) =>
            builder.RegisterComponent(_coloringResult);
        
        private void BindGuessingCapitals(IContainerBuilder builder) =>
            builder.RegisterComponent(_guessingCapitals);
        
        private void BindTutorial(IContainerBuilder builder) =>
            builder.RegisterComponent(_tutorial);
        
        private void BindLevelInfo(IContainerBuilder builder) =>
            builder.RegisterComponent(_levelInfo);
        
        private void BindDescriptionTask(IContainerBuilder builder) =>
            builder.RegisterComponent(_descriptionTask);
        
        private void BindLevelEffects(IContainerBuilder builder) =>
            builder.RegisterComponent(_levelEffects);
        
        private void BindGameResults(IContainerBuilder builder) =>
            builder.RegisterComponent(_gameResults);
    }
}