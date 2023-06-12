using ComponentSystem;
using Player.PlayerFiniteStateMachine.States.SuperStates;

namespace Player.PlayerFiniteStateMachine.States
{
    public class IdleState : ControlledState
    {
        public IdleState(Player player, PlayerStateMachine stateMachine, string animationTransitionParam) 
            : base(player, stateMachine, animationTransitionParam) {}

        public override void Enter()
        {
            base.Enter();
            
            movement.Stop();
        }

        protected override void CheckTransitions()
        {
            if (inputHandler.IsMoving)
            {
                SwitchState(PlayerStateType.Move);
            }
        }
    }
}