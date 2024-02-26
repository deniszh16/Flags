using Logic.Levels;

namespace Services.StateMachine.States
{
    public class ColoringState : BaseStates
    {
        
        private readonly DescriptionTask _descriptionTask;
        
        public ColoringState(GameStateMachine stateMachine, DescriptionTask descriptionTask) : base(stateMachine)
        {
            _descriptionTask = descriptionTask;
        }

        public override void Enter()
        {
            _descriptionTask.ChangeDescription(Description.Coloring);
        }

        public override void Exit()
        {
        }
    }
}