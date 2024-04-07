using Services.StateMachine.States;
using Services.PersistentProgress;
using Cysharp.Threading.Tasks;
using Services.UpdateService;
using Services.StateMachine;
using Logic.Levels.Coloring;
using Logic.Levels.Factory;
using Logic.UI.Tutorials;
using Logic.UI.Levels;
using Logic.UI.Hints;
using UnityEngine;
using System;
using UniRx;

namespace Logic.Levels.StateMachine.States
{
    public class ColoringState : BaseStates
    {
        private readonly IPersistentProgressService _progressService;
        private readonly IMonoUpdateService _monoUpdateService;

        private readonly IFlagFactory _flagFactory;
        private readonly DescriptionTask _descriptionTask;
        private readonly ArrangementOfColors _arrangementOfColors;

        private readonly ColoringFlag _coloringFlag;
        private readonly HintForColoring _hintForColoring;
        private readonly ColorCancellation _colorCancellation;
        private readonly ColoringResult _coloringResult;

        private readonly LevelEffects _levelEffects;
        private readonly Tutorial _tutorial;

        private readonly CompositeDisposable _compositeDisposable = new();

        public ColoringState(GameStateMachine stateMachine, IPersistentProgressService progressService, IMonoUpdateService monoUpdateService,
            IFlagFactory flagFactory, DescriptionTask descriptionTask, ArrangementOfColors arrangementOfColors, ColoringFlag coloringFlag,
            HintForColoring hintForColoring, ColorCancellation colorCancellation, ColoringResult coloringResult,
            LevelEffects levelEffects, Tutorial tutorial) : base(stateMachine)
        {
            _progressService = progressService;
            _monoUpdateService = monoUpdateService;
            
            _flagFactory = flagFactory;
            _descriptionTask = descriptionTask;
            _arrangementOfColors = arrangementOfColors;
            
            _coloringFlag = coloringFlag;
            _hintForColoring = hintForColoring;
            _colorCancellation = colorCancellation;
            _coloringResult = coloringResult;
            
            _levelEffects = levelEffects;
            _tutorial = tutorial;
        }

        public override void Enter()
        {
            _coloringFlag.Init(_monoUpdateService);
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

            ShowTutorial();
        }

        private async UniTask ResetFlagColoring()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(1f), ignoreTimeScale: false);
            _coloringFlag.ResetAllFragments();
            _arrangementOfColors.ResetColorButtons();
            _coloringResult.HideResultIcon();
        }
        
        private void ShowTutorial()
        {
            if (_progressService.GetUserProgress.Progress <= 1)
            {
                _hintForColoring.ChangeActivityOfHintButton(state: false);
                _tutorial.ShowTutorial(delay: 0.2f, position: new Vector2(-145, -720f)).Forget();
                _arrangementOfColors.ColoredButtonSelected.Subscribe(_ => _tutorial.ChangeVisibilityOfTutorial(state: false)).AddTo(_compositeDisposable);
                _arrangementOfColors.ColoredButtonSelected.Subscribe(_ =>
                {
                    _tutorial.ShowTutorial(delay: 0.2f, position: new Vector2(0, -150f)).Forget();
                }).AddTo(_compositeDisposable);
                _coloringFlag.StartedColoring.Subscribe(_ => _tutorial.ChangeVisibilityOfTutorial(state: false)).AddTo(_compositeDisposable);
            }
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