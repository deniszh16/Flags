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

        private CurrentLevel _currentLevel;
        private DescriptionTask _descriptionTask;
        private DrawingRoute _drawingRoute;

        [Inject]
        private void Construct(GameStateMachine gameStateMachine, IPersistentProgressService progressService, IStaticDataService staticData, Countries countries,
            MapProgress mapProgress, CurrentCountry currentCountry, CurrentLevel currentLevel, DescriptionTask descriptionTask, DrawingRoute drawingRoute)
        {
            _gameStateMachine = gameStateMachine;
            _progressService = progressService;
            _staticData = staticData;
            
            _countries = countries;
            _mapProgress = mapProgress;
            _currentCountry = currentCountry;

            _currentLevel = currentLevel;
            _descriptionTask = descriptionTask;
            _drawingRoute = drawingRoute;
        }

        private void Awake()
        {
            _gameStateMachine.AddState(new MapState(_gameStateMachine, _progressService, _staticData, _countries, _mapProgress, _currentCountry));
            _gameStateMachine.AddState(new DrawingState(_gameStateMachine, _progressService, _staticData, _currentLevel, _descriptionTask, _drawingRoute));
        }

        private void Start() =>
            _gameStateMachine.Enter<MapState>();
    }
}