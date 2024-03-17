using Services.PersistentProgress;
using Cysharp.Threading.Tasks;
using Logic.Levels.Coloring;
using Logic.Levels.Tutorial;
using Logic.Levels.Factory;
using Logic.Levels.Hints;
using Logic.Levels.Other;
using UnityEngine;
using System;
using UniRx;

namespace Services.StateMachine.States
{
    public class ColoringState : BaseStates
    {
        private readonly IPersistentProgressService _progressService;
        
        private readonly ColoringFlag _coloringFlag;
        private readonly ArrangementOfColors _arrangementOfColors;
        private readonly IFlagFactory _flagFactory;
        private readonly DescriptionTask _descriptionTask;
        private readonly HintForColoring _hintForColoring;
        private readonly ColorCancellation _colorCancellation;
        private readonly LevelEffects _levelEffects;
        private readonly Tutorial _tutorial;
        private readonly ColoringResult _coloringResult;
        
        private readonly CompositeDisposable _compositeDisposable = new();

        public ColoringState(GameStateMachine stateMachine, IPersistentProgressService progressService,  ColoringFlag coloringFlag,
            ArrangementOfColors arrangementOfColors, IFlagFactory flagFactory, DescriptionTask descriptionTask, HintForColoring hintForColoring,
            ColorCancellation colorCancellation, LevelEffects levelEffects, Tutorial tutorial, ColoringResult coloringResult) : base(stateMachine)
        {
            _progressService = progressService;
            _coloringFlag = coloringFlag;
            _arrangementOfColors = arrangementOfColors;
            _flagFactory = flagFactory;
            _descriptionTask = descriptionTask;
            _hintForColoring = hintForColoring;
            _colorCancellation = colorCancellation;
            _levelEffects = levelEffects;
            _tutorial = tutorial;
            _coloringResult = coloringResult;
        }

        public override void Enter()
        {
            _coloringFlag.SetFlag(_flagFactory.GetCreatedFlag);
            _coloringFlag.GetCurrentFragment();
            _coloringFlag.ChangeColoringActivity(state: true);
            _coloringFlag.StartedColoring.Subscribe(_ => _arrangementOfColors.DisableInteractivityOfAllButtons()).AddTo(_compositeDisposable);
            _coloringFlag.StartedColoring.Subscribe(_ => _arrangementOfColors.RecordSelectedColor()).AddTo(_compositeDisposable);
            _coloringFlag.StartedColoring.Subscribe(_ => _colorCancellation.ChangeButtonActivity(state: false)).AddTo(_compositeDisposable);
            _coloringFlag.FragmentIsColored.Subscribe(_ => _arrangementOfColors.ActivateInteractivityOfUnusedButtons()).AddTo(_compositeDisposable);
            _coloringFlag.FragmentIsColored.Subscribe(_ => _colorCancellation.ChangeButtonActivity(state: true)).AddTo(_compositeDisposable);
            _coloringFlag.FlagIsFinished.Subscribe(_ => _arrangementOfColors.CompareColorCollections()).AddTo(_compositeDisposable);
            _arrangementOfColors.ActivateInteractivityOfUnusedButtons();
            _arrangementOfColors.CorrectColoring.Subscribe(_ => _coloringResult.ShowVictoryIcon()).AddTo(_compositeDisposable);
            _arrangementOfColors.CorrectColoring.Subscribe(_ => _stateMachine.Enter<GuessingState>()).AddTo(_compositeDisposable);
            _arrangementOfColors.CorrectColoring.Subscribe(_ => _levelEffects.ShowColorizedFlagEffect()).AddTo(_compositeDisposable);
            _arrangementOfColors.IncorrectColoring.Subscribe(_ => _coloringResult.ShowLossIcon()).AddTo(_compositeDisposable);
            _arrangementOfColors.IncorrectColoring.Subscribe(_ => _colorCancellation.ChangeButtonActivity(state: false)).AddTo(_compositeDisposable);
            _arrangementOfColors.IncorrectColoring.Subscribe(_ => ResetFlagColoring().Forget()).AddTo(_compositeDisposable);
            _arrangementOfColors.IncorrectColoring.Subscribe(_ => _levelEffects.ShowEffectIncorrectColoring()).AddTo(_compositeDisposable);
            _descriptionTask.ChangeDescription(DescriptionTypes.Coloring);
            _hintForColoring.ChangeActivityOfHintButton(state: true);

            if (_progressService.GetUserProgress.Progress <= 1)
            {
                _tutorial.ShowTutorial(delay: 0.2f, position: new Vector2(400f, -750f)).Forget();
                _arrangementOfColors.ColoredButtonSelected.Subscribe(_ => _tutorial.ChangeVisibilityOfTutorial(state: false)).AddTo(_compositeDisposable);
                _arrangementOfColors.ColoredButtonSelected.Subscribe(_ =>
                {
                    _tutorial.ShowTutorial(delay: 0.2f, position: new Vector2(0, -150f)).Forget();
                }).AddTo(_compositeDisposable);
                _coloringFlag.StartedColoring.Subscribe(_ => _tutorial.ChangeVisibilityOfTutorial(state: false)).AddTo(_compositeDisposable);
            }
        }

        private async UniTask ResetFlagColoring()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(1f), ignoreTimeScale: false);
            _coloringFlag.ResetAllFragments();
            _arrangementOfColors.ResetColorButtons();
            _coloringResult.HideResultIcon();
        }

        public override void Exit()
        {
            _compositeDisposable.Clear();
            _coloringFlag.ChangeColoringActivity(state: false);
            _hintForColoring.ChangeActivityOfHintButton(state: false);
            _colorCancellation.ChangeButtonActivity(state: false);
            _arrangementOfColors.ResetColorButtons();
            _arrangementOfColors.ChangeVisibilityOfColors(state: false);
            _arrangementOfColors.DisableInteractivityOfAllButtons();
        }
    }
}