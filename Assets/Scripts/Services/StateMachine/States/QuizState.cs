using UnityEngine;

namespace Services.StateMachine.States
{
    public class QuizState : BaseStates
    {
        public QuizState(GameStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            Debug.Log("Enter QuizState");
        }

        public override void Exit()
        {
        }
    }
}