using Logic.Levels.StateMachine.States;
using Services.PersistentProgress;
using Services.StaticDataService;
using Services.UpdateService;
using Services.StateMachine;
using Logic.Levels.Guessing;
using Logic.Levels.Coloring;
using Logic.Levels.Drawing;
using Logic.Levels.Factory;
using Logic.UI.Tutorials;
using Services.SaveLoad;
using Logic.UI.Buttons;
using Logic.UI.Levels;
using Logic.WorldMap;
using Logic.UI.Hints;
using UnityEngine;
using Zenject;

namespace Logic.Levels.StateMachine
{
    public class LevelStateMachine : MonoBehaviour
    {
        private GameStateMachine _gameStateMachine;
        private IMonoUpdateService _monoUpdateService;
        private IPersistentProgressService _progressService;
        private ISaveLoadService _saveLoadService;
        private IStaticDataService _staticData;

        private Countries _countries;
        private MapProgress _mapProgress;
        private CurrentCountry _currentCountry;
        private LevelStartButton _levelStartButton;

        private IFlagFactory _flagFactory;
        private DrawingSection _drawingSection;
        private DrawingRoute _drawingRoute;
        private DescriptionTask _descriptionTask;
        private InfoCurrentLevel _infoCurrentLevel;
        private ArrangementOfColors _arrangementOfColors;

        private ColoringFlag _coloringFlag;
        private HintForColoring _hintForColoring;
        private ColorCancellation _colorCancellation;
        private ColoringResult _coloringResult;

        private GuessingCapitals _guessingCapitals;
        
        private LevelEffects _levelEffects;
        private Tutorial _tutorial;
        private GameResults _gameResults;

        [Inject]
        private void Construct(GameStateMachine gameStateMachine, IMonoUpdateService monoUpdateService, IPersistentProgressService progressService,
            ISaveLoadService saveLoadService, IStaticDataService staticData, Countries countries, MapProgress mapProgress, CurrentCountry currentCountry,
            LevelStartButton levelStartButton, IFlagFactory flagFactory, DrawingSection drawingSection, DrawingRoute drawingRoute, DescriptionTask descriptionTask,
            InfoCurrentLevel infoCurrentLevel, ArrangementOfColors arrangementOfColors, ColoringFlag coloringFlag, HintForColoring hintForColoring,
            ColorCancellation colorCancellation, ColoringResult coloringResult, GuessingCapitals guessingCapitals, LevelEffects levelEffects,
            Tutorial tutorial, GameResults gameResults)
        {
            _gameStateMachine = gameStateMachine;
            _monoUpdateService = monoUpdateService;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
            _staticData = staticData;
            
            _countries = countries;
            _mapProgress = mapProgress;
            _currentCountry = currentCountry;
            _levelStartButton = levelStartButton;

            _flagFactory = flagFactory;
            _drawingSection = drawingSection;
            _drawingRoute = drawingRoute;
            _descriptionTask = descriptionTask;
            _infoCurrentLevel = infoCurrentLevel;
            _arrangementOfColors = arrangementOfColors;

            _coloringFlag = coloringFlag;
            _hintForColoring = hintForColoring;
            _colorCancellation = colorCancellation;
            _coloringResult = coloringResult;

            _guessingCapitals = guessingCapitals;
            
            _levelEffects = levelEffects;
            _tutorial = tutorial;
            _gameResults = gameResults;
        }

        private void Awake()
        {
            _gameStateMachine.AddState(new MapState(_gameStateMachine, _progressService, _staticData, _countries, _mapProgress, _currentCountry, _levelStartButton));
            _gameStateMachine.AddState(new DrawingState(_gameStateMachine, _progressService, _staticData, _monoUpdateService, _flagFactory, _drawingSection, _drawingRoute, _descriptionTask, _infoCurrentLevel, _arrangementOfColors, _hintForColoring, _levelEffects, _tutorial));
            _gameStateMachine.AddState(new ColoringState(_gameStateMachine, _progressService, _monoUpdateService, _flagFactory, _descriptionTask, _arrangementOfColors, _coloringFlag, _hintForColoring, _colorCancellation, _coloringResult, _levelEffects, _tutorial));
            _gameStateMachine.AddState(new GuessingState(_gameStateMachine, _staticData, _progressService, _drawingSection, _descriptionTask, _coloringResult, _guessingCapitals, _levelEffects, _tutorial));
            _gameStateMachine.AddState(new ResultsState(_gameStateMachine, _progressService, _saveLoadService, _flagFactory, _coloringResult, _levelEffects, _gameResults));
        }

        private void Start() =>
            _gameStateMachine.Enter<MapState>();
    }
}