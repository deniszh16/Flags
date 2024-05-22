using Cysharp.Threading.Tasks;
using DZGames.Flags.Services;
using UnityEngine;
using System;
using UniRx;

namespace DZGames.Flags.Logic
{
    public class GuessingState : BaseStates
    {
        private readonly IStaticDataService _staticData;
        private readonly IPersistentProgressService _progressService;

        private readonly DrawingSection _drawingSection;
        private readonly ColoringResult _coloringResult;
        private readonly GuessingCapitals _guessingCapitals;

        private readonly Tutorial _tutorial;
        private readonly DescriptionTask _descriptionTask;
        private readonly LevelEffects _levelEffects;

        private readonly CompositeDisposable _compositeDisposable = new();
        
        public GuessingState(GameStateMachine stateMachine, IStaticDataService staticData, IPersistentProgressService progressService,
            DrawingSection drawingSection, ColoringResult coloringResult, GuessingCapitals guessingCapitals, Tutorial tutorial, 
            DescriptionTask descriptionTask, LevelEffects levelEffects) : base(stateMachine)
        {
            _staticData = staticData;
            _progressService = progressService;

            _drawingSection = drawingSection;
            _coloringResult = coloringResult;
            _guessingCapitals = guessingCapitals;

            _tutorial = tutorial;
            _descriptionTask = descriptionTask;
            _levelEffects = levelEffects;
        }

        public override void Enter()
        {
            _guessingCapitals.ChangeGuessingActivity(state: true);
            _guessingCapitals.ArrangeOptions(_staticData.GetLevelConfig().LevelConfig[_progressService.GetUserProgress.Progress - 1].Capitals,
                _staticData.GetLevelConfig().LevelConfig[_progressService.GetUserProgress.Progress - 1].CorrectVariant);
            _guessingCapitals.ShowSpawnAnimation();
            _guessingCapitals.QuizCompleted.Subscribe(answer => GoToResultsAsync(answer).Forget()).AddTo(_compositeDisposable);
            _guessingCapitals.QuizCompleted.Subscribe(UpdateScreenValues).AddTo(_compositeDisposable);
            _guessingCapitals.QuizCompleted.Subscribe(answer => _levelEffects.ShowEffectQuizResult(answer)).AddTo(_compositeDisposable);
            
            _descriptionTask.ChangeDescription(DescriptionTypes.Guessing);

            ShowTutorial();
        }
        
        public override void Exit()
        {
            _compositeDisposable.Clear();
            _drawingSection.ChangeVisibilityOfDrawingSection(state: false);
            _guessingCapitals.ChangeGuessingActivity(state: false);
            _guessingCapitals.ResetPanelCapitals();
        }

        private void UpdateScreenValues(bool state)
        {
            DescriptionTypes text = state ? DescriptionTypes.CorrectAnswer : DescriptionTypes.IncorrectAnswer;
            _descriptionTask.ChangeDescription(text);
            
            if (state) return;
            _coloringResult.HideResultIcon();
            _coloringResult.ShowLossIcon();
        }

        private async UniTask GoToResultsAsync(bool answer)
        {
            _progressService.GetUserProgress.ChangeNumberOfAnswers(answer);
            await UniTask.Delay(TimeSpan.FromSeconds(1.3f), ignoreTimeScale: false);
            _stateMachine.Enter<ResultsState>();
        }

        private void ShowTutorial()
        {
            if (_progressService.GetUserProgress.Progress <= 1)
            {
                _tutorial.ShowTutorialAsync(delay: 0.4f, position: new Vector2(235f, -650f)).Forget();
                _guessingCapitals.QuizCompleted.Subscribe(answer => _tutorial.ChangeVisibilityOfTutorial(state: false)).AddTo(_compositeDisposable);
            }
        }
    }
}