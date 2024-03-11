using Services.StateMachine;
using Logic.Levels.Coloring;
using Logic.Levels.Guessing;
using Logic.Levels.Drawing;
using Logic.Levels.Factory;
using Logic.Levels.Hints;
using Logic.Levels.Other;
using Logic.WorldMap;
using Logic.Buttons;
using UnityEngine;
using Zenject;

namespace Logic.Levels
{
    public class LevelsInstaller : MonoInstaller
    {
        [SerializeField] private Countries _countries;
        [SerializeField] private MapProgress _mapProgress;
        [SerializeField] private CurrentCountry _currentCountry;
        [SerializeField] private LevelStartButton _levelStartButton;

        [SerializeField] private DrawingSection _drawingSection;
        [SerializeField] private DrawingRoute _drawingRoute;
        [SerializeField] private DescriptionTask _descriptionTask;
        [SerializeField] private ArrangementOfColors _arrangementOfColors;

        [SerializeField] private ColoringFlag _coloringFlag;
        [SerializeField] private HintForColoring _hintForColoring;
        [SerializeField] private ColorCancellation _colorCancellation;
        [SerializeField] private InfoCurrentLevel _levelInfo;
        [SerializeField] private ColoringResult _coloringResult;
        
        [SerializeField] private GuessingCapitals _guessingCapitals;
        [SerializeField] private LevelEffects _levelEffects;
        [SerializeField] private GameResults _gameResults;

        public override void InstallBindings()
        {
            BindGameStateMachine();
            
            BindCountries();
            BindMapProgress();
            BindCurrentCountry();
            BindLevelStartButton();

            BindFlagFactory();
            BindDrawingSection();
            BindDrawingRoute();
            BindDescriptionTask();
            BindArrangementOfButtonsColor();

            BindColoringFlag();
            BindHintForColoring();
            BindColorCancellation();
            BindLevelInfo();
            BindColoringResult();

            BindGuessingCapitals();
            BindLevelEffects();
            BindGameResults();
        }
        
        private void BindGameStateMachine()
        {
            GameStateMachine gameStateMachine = new GameStateMachine();
            Container.BindInstance(gameStateMachine).AsSingle();
        }

        private void BindCountries() =>
            Container.BindInstance(_countries).AsSingle();
        
        private void BindMapProgress() =>
            Container.BindInstance(_mapProgress).AsSingle();
        
        private void BindCurrentCountry() =>
            Container.BindInstance(_currentCountry).AsSingle();

        private void BindLevelStartButton() =>
            Container.BindInstance(_levelStartButton).AsSingle();

        private void BindFlagFactory()
        {
            IFlagFactory flagFactory = new FlagFactory();
            Container.BindInstance(flagFactory).AsSingle();
        }
        
        private void BindDrawingSection() =>
            Container.BindInstance(_drawingSection).AsSingle();

        private void BindDrawingRoute() =>
            Container.BindInstance(_drawingRoute).AsSingle();

        private void BindDescriptionTask() =>
            Container.BindInstance(_descriptionTask).AsSingle();
        
        private void BindArrangementOfButtonsColor() =>
            Container.BindInstance(_arrangementOfColors).AsSingle();
        
        private void BindColoringFlag() =>
            Container.BindInstance(_coloringFlag).AsSingle();
        
        private void BindHintForColoring() =>
            Container.BindInstance(_hintForColoring).AsSingle();
        
        private void BindColorCancellation() =>
            Container.BindInstance(_colorCancellation).AsSingle();
        
        private void BindLevelInfo() =>
            Container.BindInstance(_levelInfo).AsSingle();

        private void BindColoringResult() =>
            Container.BindInstance(_coloringResult).AsSingle();

        private void BindGuessingCapitals() =>
            Container.BindInstance(_guessingCapitals).AsSingle();

        private void BindLevelEffects() =>
            Container.BindInstance(_levelEffects).AsSingle();

        private void BindGameResults() =>
            Container.BindInstance(_gameResults).AsSingle();
    }
}