using UnityEngine;

namespace Services.StateMachine.States
{
    public class ResultsState : BaseStates
    {
        public ResultsState(GameStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            Debug.Log("ResultsState");
        }

        public override void Exit()
        {
        }
    }
}