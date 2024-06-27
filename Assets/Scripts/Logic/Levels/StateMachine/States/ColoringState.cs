using Cysharp.Threading.Tasks;
using Flags.Services;
using UnityEngine;
using System;
using UniRx;

namespace Flags.Logic
{
    public class ColoringState : BaseStates
    {
        private readonly IPersistentProgressService _progressService;
        private readonly IFlagFactory _flagFactory;
        
        private readonly ArrangementOfColors _arrangementOfColors;
        private readonly ColoringFlag _coloringFlag;
        private readonly HintForColoring _hintForColoring;
        private readonly ColorCancellation _colorCancellation;
        private readonly ColoringResult _coloringResult;

        private readonly Tutorial _tutorial;
        private readonly DescriptionTask _descriptionTask;
        private readonly LevelEffects _levelEffects;

        private readonly CompositeDisposable _compositeDisposable = new();

        public ColoringState(GameStateMachine stateMachine, IPersistentProgressService progressService, IFlagFactory flagFactory,
            ArrangementOfColors arrangementOfColors, ColoringFlag coloringFlag, HintForColoring hintForColoring, ColorCancellation colorCancellation,
            ColoringResult coloringResult, Tutorial tutorial, DescriptionTask descriptionTask, LevelEffects levelEffects) : base(stateMachine)
        {
            _progressService = progressService;
            _flagFactory = flagFactory;
            
            _arrangementOfColors = arrangementOfColors;
            _coloringFlag = coloringFlag;
            _hintForColoring = hintForColoring;
            _colorCancellation = colorCancellation;
            _coloringResult = coloringResult;

            _tutorial = tutorial;
            _descriptionTask = descriptionTask;
            _levelEffects = levelEffects;
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
            _arrangementOfColors.IncorrectColoring.Subscribe(_ => ResetFlagColoringAsync().Forget()).AddTo(_compositeDisposable);
            _arrangementOfColors.IncorrectColoring.Subscribe(_ => _levelEffects.ShowEffectIncorrectColoring()).AddTo(_compositeDisposable);
            
            _descriptionTask.ChangeDescription(DescriptionTypes.Coloring);
            _hintForColoring.ChangeActivityOfHintButton(state: true);

            ShowTutorial();
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

        private async UniTask ResetFlagColoringAsync()
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
                _tutorial.ShowTutorialAsync(delay: 0.2f, position: new Vector2(-145, -720f)).Forget();
                _arrangementOfColors.ColoredButtonSelected.Subscribe(_ => _tutorial.ChangeVisibilityOfTutorial(state: false)).AddTo(_compositeDisposable);
                _arrangementOfColors.ColoredButtonSelected.Subscribe(_ =>
                {
                    _tutorial.ShowTutorialAsync(delay: 0.2f, position: new Vector2(0, -150f)).Forget();
                }).AddTo(_compositeDisposable);
                _coloringFlag.StartedColoring.Subscribe(_ => _tutorial.ChangeVisibilityOfTutorial(state: false)).AddTo(_compositeDisposable);
            }
        }
    }
}