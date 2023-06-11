using Player.PlayerFiniteStateMachine.States.SuperStates;
using UnityEngine;

namespace Player.PlayerFiniteStateMachine.States
{
    public class MoveState : ControlledState
    {
        private float _movementSpeed;

        public MoveState(Player player, PlayerStateMachine stateMachine, int hashedAnimatorParam) 
            : base(player, stateMachine, hashedAnimatorParam)
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