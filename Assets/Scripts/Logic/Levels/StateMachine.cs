using Services.StateMachine.States;
using Services.PersistentProgress;
using Services.StaticDataService;
using Services.StateMachine;
using Services.SaveLoad;
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
    public class StateMachine : MonoBehaviour
    {
        private GameStateMachine _gameStateMachine;
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
        private GameResults _gameResults;

        [Inject]
        private void Construct(GameStateMachine gameStateMachine, IPersistentProgressService progressService, ISaveLoadService saveLoadService, IStaticDataService staticData,
            Countries countries, MapProgress mapProgress, CurrentCountry currentCountry, LevelStartButton levelStartButton, IFlagFactory flagFactory,
            DrawingSection drawingSection, DrawingRoute drawingRoute, DescriptionTask descriptionTask, InfoCurrentLevel infoCurrentLevel, ArrangementOfColors arrangementOfColors,
            ColoringFlag coloringFlag, HintForColoring hintForColoring, ColorCancellation colorCancellation, ColoringResult coloringResult, GuessingCapitals guessingCapitals,
            GameResults gameResults)
        {
            _gameStateMachine = gameStateMachine;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
            _staticData = staticData;
            
            _countries = countries;
            _mapProgress = mapProgress;
            _currentCountry = currentCountry;

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
            _gameResults = gameResults;
        }

        private void Awake()
        {
            _gameStateMachine.AddState(new MapState(_gameStateMachine, _progressService, _staticData, _countries, _mapProgress, _currentCountry, _levelStartButton));
            _gameStateMachine.AddState(new DrawingState(_gameStateMachine, _progressService, _staticData, _flagFactory, _drawingSection, _drawingRoute, _descriptionTask, _infoCurrentLevel, _arrangementOfColors, _hintForColoring));
            _gameStateMachine.AddState(new ColoringState(_gameStateMachine, _coloringFlag, _arrangementOfColors, _flagFactory, _descriptionTask, _hintForColoring, _colorCancellation, _coloringResult));
            _gameStateMachine.AddState(new GuessingState(_gameStateMachine, _staticData, _progressService, _guessingCapitals, _descriptionTask, _drawingSection));
            _gameStateMachine.AddState(new ResultsState(_gameStateMachine, _progressService, _saveLoadService, _gameResults, _flagFactory, _coloringResult));
        }

        private void Start() =>
            _gameStateMachine.Enter<MapState>();
    }
}