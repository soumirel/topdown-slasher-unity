using Player.PlayerFiniteStateMachine.States.SuperStates;
using UnityEngine;

namespace Player.PlayerFiniteStateMachine.States
{
    public class MoveState : ControlledState
    {
        private float _movementSpeed;

        public MoveState(Player player, PlayerStateMachine stateMachine, string animationTransitionParam) 
            : base(player, stateMachine, animationTransitionParam)
        {
            _movementSpeed = player.MovementSpeed;
        }

        protected override void CheckTransitions()
        {
            if (!player.InputHandler.IsMoving)
            {
                SwitchState(PlayerStateType.Idle);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            
            movement.Move(inputHandler.MovementDirection);
        }
    }
}