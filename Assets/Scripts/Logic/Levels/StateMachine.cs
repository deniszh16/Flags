using Logic.Levels.Factory;
using Services.StateMachine.States;
using Services.PersistentProgress;
using Services.StaticDataService;
using Services.StateMachine;
using Logic.WorldMap;
using UnityEngine;
using Zenject;

namespace Logic.Levels
{
    public class StateMachine : MonoBehaviour
    {
        private GameStateMachine _gameStateMachine;
        private IPersistentProgressService _progressService;
        private IStaticDataService _staticData;

        private Countries _countries;
        private MapProgress _mapProgress;
        private CurrentCountry _currentCountry;

        private IFlagFactory _flagFactory;
        private DrawingSection _drawingSection;
        private DrawingRoute _drawingRoute;
        private DescriptionTask _descriptionTask;
        private ArrangementOfColors _arrangementOfColors;

        [Inject]
        private void Construct(GameStateMachine gameStateMachine, IPersistentProgressService progressService, IStaticDataService staticData, Countries countries,
            MapProgress mapProgress, CurrentCountry currentCountry, IFlagFactory flagFactory, DrawingSection drawingSection, DrawingRoute drawingRoute,
            DescriptionTask descriptionTask, ArrangementOfColors arrangementOfColors)
        {
            _gameStateMachine = gameStateMachine;
            _progressService = progressService;
            _staticData = staticData;
            
            _countries = countries;
            _mapProgress = mapProgress;
            _currentCountry = currentCountry;

            _flagFactory = flagFactory;
            _drawingSection = drawingSection;
            _drawingRoute = drawingRoute;
            _descriptionTask = descriptionTask;
            _arrangementOfColors = arrangementOfColors;
        }

        private void Awake()
        {
            _gameStateMachine.AddState(new MapState(_gameStateMachine, _progressService, _staticData, _countries, _mapProgress, _currentCountry));
            _gameStateMachine.AddState(new DrawingState(_gameStateMachine, _progressService, _staticData, _flagFactory, _drawingSection, _drawingRoute, _descriptionTask, _arrangementOfColors));
            _gameStateMachine.AddState(new ColoringState(_gameStateMachine, _descriptionTask));
        }

        private void Start() =>
            _gameStateMachine.Enter<MapState>();
    }
}