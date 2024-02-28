using Logic.Levels;
using Logic.Levels.Factory;

namespace Services.StateMachine.States
{
    public class ColoringState : BaseStates
    {
        private readonly ArrangementOfColors _arrangementOfColors;
        private readonly ColoringFlag _coloringFlag;
        private readonly IFlagFactory _flagFactory;
        private readonly DescriptionTask _descriptionTask;

        public ColoringState(GameStateMachine stateMachine, ArrangementOfColors arrangementOfColors, ColoringFlag coloringFlag, IFlagFactory flagFactory,
            DescriptionTask descriptionTask) : base(stateMachine)
        {
            _arrangementOfColors = arrangementOfColors;
            _coloringFlag = coloringFlag;
            _flagFactory = flagFactory;
            _descriptionTask = descriptionTask;
        }

        public override void Enter()
        {
            _coloringFlag.SetFlag(_flagFactory.GetCreatedFlag);
            _coloringFlag.GetCurrentFragment();
            _coloringFlag.ChangeColoringActivity(state: true);
            _coloringFlag.StartedColoring += _arrangementOfColors.DisableAllButtons;
            _coloringFlag.StartedColoring += _arrangementOfColors.RecordSelectedColor;
            _coloringFlag.FinishedColoring += _arrangementOfColors.ActivateUnusedButtons;
            _coloringFlag.FlagIsFinished += GoToStateQuiz;
            _arrangementOfColors.PrepareListOfUsedColors();
            _arrangementOfColors.ActivateUnusedButtons();
            _descriptionTask.ChangeDescription(Description.Coloring);
        }
        
        private void GoToStateQuiz() =>
            _stateMachine.Enter<QuizState>();

        public override void Exit()
        {
            _coloringFlag.ChangeColoringActivity(state: false);
            _coloringFlag.StartedColoring -= _arrangementOfColors.DisableAllButtons;
            _coloringFlag.StartedColoring -= _arrangementOfColors.RecordSelectedColor;
            _coloringFlag.FinishedColoring -= _arrangementOfColors.ActivateUnusedButtons;
            _coloringFlag.FlagIsFinished -= GoToStateQuiz;
        }
    }
}