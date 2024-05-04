using DZGames.Flags.Services;
using UnityEngine;
using VContainer;

namespace DZGames.Flags.Logic
{
    public class LevelStateMachine : MonoBehaviour
    {
        private GameStateMachine _gameStateMachine;
        private IPersistentProgressService _progressService;
        private ISaveLoadService _saveLoadService;
        private IStaticDataService _staticDataService;
        private IFlagFactory _flagFactory;
        
        private Countries _countries;
        private MapProgress _mapProgress;
        private CurrentCountry _currentCountry;
        private LevelStartButton _levelStartButton;
        
        private DrawingSection _drawingSection;
        private DrawingRoute _drawingRoute;
        
        private ArrangementOfColors _arrangementOfColors;
        private ColoringFlag _coloringFlag;
        private HintForColoring _hintForColoring;
        private ColorCancellation _colorCancellation;
        private ColoringResult _coloringResult;
        
        private GuessingCapitals _guessingCapitals;
        
        private Tutorial _tutorial;
        private InfoCurrentLevel _infoCurrentLevel;
        private DescriptionTask _descriptionTask;
        private LevelEffects _levelEffects;
        private GameResults _gameResults;
        
        [Inject]
        private void Construct(GameStateMachine gameStateMachine, IPersistentProgressService progressService, ISaveLoadService saveLoadService, IStaticDataService staticData,
            IFlagFactory flagFactory, Countries countries, MapProgress mapProgress, CurrentCountry currentCountry, LevelStartButton levelStartButton,
            DrawingSection drawingSection, DrawingRoute drawingRoute, ArrangementOfColors arrangementOfColors, ColoringFlag coloringFlag, HintForColoring hintForColoring,
            ColorCancellation colorCancellation, ColoringResult coloringResult, GuessingCapitals guessingCapitals, Tutorial tutorial, InfoCurrentLevel infoCurrentLevel,
            DescriptionTask descriptionTask, LevelEffects levelEffects, GameResults gameResults)
        {
            _gameStateMachine = gameStateMachine;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
            _staticDataService = staticData;
            _flagFactory = flagFactory;
            
            _countries = countries;
            _mapProgress = mapProgress;
            _currentCountry = currentCountry;
            _levelStartButton = levelStartButton;
            
            _drawingSection = drawingSection;
            _drawingRoute = drawingRoute;

            _arrangementOfColors = arrangementOfColors;
            _coloringFlag = coloringFlag;
            _hintForColoring = hintForColoring;
            _colorCancellation = colorCancellation;
            _coloringResult = coloringResult;

            _guessingCapitals = guessingCapitals;

            _tutorial = tutorial;
            _infoCurrentLevel = infoCurrentLevel;
            _descriptionTask = descriptionTask;
            _levelEffects = levelEffects;
            _gameResults = gameResults;
        }
        
        private void Awake()
        {
            _gameStateMachine.AddState(new MapState(_gameStateMachine, _progressService, _staticDataService, _countries, _mapProgress, _currentCountry, _levelStartButton));
            _gameStateMachine.AddState(new DrawingState(_gameStateMachine, _progressService, _staticDataService, _flagFactory, _drawingSection, _drawingRoute, _arrangementOfColors, _hintForColoring, _tutorial, _infoCurrentLevel, _descriptionTask, _levelEffects));
            _gameStateMachine.AddState(new ColoringState(_gameStateMachine, _progressService, _flagFactory, _arrangementOfColors, _coloringFlag, _hintForColoring, _colorCancellation, _coloringResult, _tutorial, _descriptionTask, _levelEffects));
            _gameStateMachine.AddState(new GuessingState(_gameStateMachine, _staticDataService, _progressService, _drawingSection, _coloringResult, _guessingCapitals, _tutorial, _descriptionTask, _levelEffects));
            _gameStateMachine.AddState(new ResultsState(_gameStateMachine, _progressService, _saveLoadService, _flagFactory, _coloringResult, _levelEffects, _gameResults));
        }
        
        private void Start() =>
            _gameStateMachine.Enter<MapState>();
    }
}