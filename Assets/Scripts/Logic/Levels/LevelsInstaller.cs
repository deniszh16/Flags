using Services.StateMachine;
using Logic.WorldMap;
using UnityEngine;
using Zenject;

namespace Logic.Levels
{
    public class LevelsInstaller : MonoInstaller
    {
        [SerializeField] private Countries _countries;
        [SerializeField] private MapProgress _mapProgress;
        [SerializeField] private CurrentCountry _currentCountry;

        [SerializeField] private CurrentLevel _currentLevel;
        [SerializeField] private DescriptionTask _descriptionTask;
        [SerializeField] private DrawingRoute _drawingRoute;
        
        public override void InstallBindings()
        {
            BindGameStateMachine();
            
            BindCountries();
            BindMapProgress();
            BindCurrentCountry();
            
            BindCurrentLevel();
            BindDescriptionTask();
            BindDrawingRoute();
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
        
        private void BindCurrentLevel() =>
            Container.BindInstance(_currentLevel).AsSingle();
        
        private void BindDescriptionTask() =>
            Container.BindInstance(_descriptionTask).AsSingle();
        
        private void BindDrawingRoute() =>
            Container.BindInstance(_drawingRoute).AsSingle();
    }
}