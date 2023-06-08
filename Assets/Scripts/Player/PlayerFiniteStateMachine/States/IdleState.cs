using ComponentSystem;
using Player.PlayerFiniteStateMachine.States.SuperStates;

namespace Player.PlayerFiniteStateMachine.States
{
    public class IdleState : ControlledState
    {
        public IdleState(PlayerController player, PlayerStateMachine stateMachine, int hashedAnimatorParam) 
            : base(player, stateMachine, hashedAnimatorParam) {}

        public override void Enter()
        {
            base.Enter();
            
            MovementCore.SetVelocityZero();
        }

        protected override void CheckTransitions()
        {
            base.CheckTransitions();

            if (player.InputHandler.IsMoving)
            {
                SwitchState(PlayerStateType.Move);
            }
        }
    }
}