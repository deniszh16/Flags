using Services.PersistentProgress;
using Services.StaticDataService;
using Cysharp.Threading.Tasks;
using Logic.Levels.Coloring;
using Logic.Levels.Guessing;
using Logic.Levels.Tutorial;
using Logic.Levels.Drawing;
using Logic.Levels.Other;
using UnityEngine;
using System;
using UniRx;

namespace Services.StateMachine.States
{
    public class GuessingState : BaseStates
    {
        private readonly IStaticDataService _staticData;
        private readonly IPersistentProgressService _progressService;
        
        private readonly GuessingCapitals _guessingCapitals;
        private readonly DescriptionTask _descriptionTask;
        private readonly DrawingSection _drawingSection;
        private readonly LevelEffects _levelEffects;
        private readonly Tutorial _tutorial;
        private readonly ColoringResult _coloringResult;

        private readonly CompositeDisposable _compositeDisposable = new();
        
        public GuessingState(GameStateMachine stateMachine, IStaticDataService staticData, IPersistentProgressService progressService, GuessingCapitals guessingCapitals,
            DescriptionTask descriptionTask, DrawingSection drawingSection, LevelEffects levelEffects, Tutorial tutorial, ColoringResult coloringResult) : base(stateMachine)
        {
            _staticData = staticData;
            _progressService = progressService;
            
            _guessingCapitals = guessingCapitals;
            _descriptionTask = descriptionTask;
            _drawingSection = drawingSection;
            _levelEffects = levelEffects;
            _tutorial = tutorial;
            _coloringResult = coloringResult;
        }

        public override void Enter()
        {
            _guessingCapitals.ChangeGuessingActivity(state: true);
            _guessingCapitals.ArrangeOptions(_staticData.GetLevelConfig().LevelConfig[_progressService.GetUserProgress.Progress - 1].Capitals,
                _staticData.GetLevelConfig().LevelConfig[_progressService.GetUserProgress.Progress - 1].CorrectVariant);
            _guessingCapitals.ShowSpawnAnimation();
            _guessingCapitals.QuizCompleted.Subscribe(answer => GoToResults(answer).Forget()).AddTo(_compositeDisposable);
            _guessingCapitals.QuizCompleted.Subscribe(UpdateScreenValues).AddTo(_compositeDisposable);
            _guessingCapitals.QuizCompleted.Subscribe(answer => _levelEffects.ShowEffectQuizResult(answer)).AddTo(_compositeDisposable);
            _descriptionTask.ChangeDescription(DescriptionTypes.Guessing);

            if (_progressService.GetUserProgress.Progress <= 1)
            {
                _tutorial.ShowTutorial(delay: 0.4f, position: new Vector2(235f, -650f)).Forget();
                _guessingCapitals.QuizCompleted.Subscribe(answer => _tutorial.ChangeVisibilityOfTutorial(state: false)).AddTo(_compositeDisposable);
            }
        }

        private void UpdateScreenValues(bool state)
        {
            DescriptionTypes text = state ? DescriptionTypes.CorrectAnswer : DescriptionTypes.IncorrectAnswer;
            _descriptionTask.ChangeDescription(text);
            
            if (state) return;
            _coloringResult.HideResultIcon();
            _coloringResult.ShowLossIcon();
        }

        private async UniTask GoToResults(bool answer)
        {
            _progressService.GetUserProgress.ChangeNumberOfAnswers(answer);
            await UniTask.Delay(TimeSpan.FromSeconds(1.3f), ignoreTimeScale: false);
            _stateMachine.Enter<ResultsState>();
        }

        public override void Exit()
        {
            _compositeDisposable.Clear();
            _drawingSection.ChangeVisibilityOfDrawingSection(state: false);
            _guessingCapitals.ChangeGuessingActivity(state: false);
        }
    }
}