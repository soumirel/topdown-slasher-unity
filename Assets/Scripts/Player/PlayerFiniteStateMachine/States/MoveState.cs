using Player.PlayerFiniteStateMachine.States.SuperStates;
using UnityEngine;

namespace Player.PlayerFiniteStateMachine.States
{
    public class MoveState : ControlledState
    {
        private float _movementSpeed;

        public MoveState(PlayerController player, PlayerStateMachine stateMachine, int hashedAnimatorParam) 
            : base(player, stateMachine, hashedAnimatorParam)
        {
            _movementSpeed = player.Data.MovementSpeed;
        }

        protected override void CheckTransitions()
        {
            base.CheckTransitions();
            
            if (!player.InputHandler.IsMoving)
            {
                SwitchState(PlayerStateType.Idle);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            
            Movement?.SetVelocity(_movementSpeed, player.InputHandler.MovementDirection);
        }
    }
}