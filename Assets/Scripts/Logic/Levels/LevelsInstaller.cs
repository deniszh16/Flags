using Logic.Levels.Factory;
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

        [SerializeField] private DrawingSection _drawingSection;
        [SerializeField] private DrawingRoute _drawingRoute;
        [SerializeField] private DescriptionTask _descriptionTask;
        [SerializeField] private ArrangementOfColors _arrangementOfColors;
        
        public override void InstallBindings()
        {
            BindGameStateMachine();
            
            BindCountries();
            BindMapProgress();
            BindCurrentCountry();

            BindFlagFactory();
            BindDrawingSection();
            BindDrawingRoute();
            BindDescriptionTask();
            BindArrangementOfButtonsColor();
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
    }
}