using Logic.Levels.Guessing;
using Logic.Levels.Other;
using Services.PersistentProgress;
using Services.StaticDataService;
using UniRx;

namespace Services.StateMachine.States
{
    public class GuessingState : BaseStates
    {
        private readonly IStaticDataService _staticData;
        private readonly IPersistentProgressService _progressService;
        
        private readonly GuessingCapitals _guessingCapitals;
        private readonly DescriptionTask _descriptionTask;

        private readonly CompositeDisposable _compositeDisposable = new();
        
        public GuessingState(GameStateMachine stateMachine, IStaticDataService staticData, IPersistentProgressService progressService,
            GuessingCapitals guessingCapitals, DescriptionTask descriptionTask) : base(stateMachine)
        {
            _staticData = staticData;
            _progressService = progressService;
            
            _guessingCapitals = guessingCapitals;
            _descriptionTask = descriptionTask;
        }

        public override void Enter()
        {
            _guessingCapitals.ChangeGuessingActivity(state: true);
            _guessingCapitals.ArrangeOptions(_staticData.GetLevelConfig().LevelConfig[_progressService.GetUserProgress.Progress - 1].Capitals,
                _staticData.GetLevelConfig().LevelConfig[_progressService.GetUserProgress.Progress - 1].CorrectVariant);
            _guessingCapitals.ShowSpawnAnimation();
            _guessingCapitals.QuizCompleted.Subscribe(_ => _stateMachine.Enter<ResultsState>()).AddTo(_compositeDisposable);
            _descriptionTask.ChangeDescription(DescriptionTypes.Guessing);
        }

        public override void Exit() =>
            _compositeDisposable.Dispose();
    }
}