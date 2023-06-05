
using ComponentSystem;
using UnityEngine;

namespace Player.PlayerFiniteStateMachine.States
{
    public class MoveState : PlayerState
    {
        private float _movementSpeed;

        public MoveState(PlayerController player, PlayerStateMachine stateMachine, int hashedAnimatorParam)
            : base(player, stateMachine, hashedAnimatorParam)
        {
            _movementSpeed = player.Data.MovementSpeed;
        }
        
        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            player.Animator.SetFloat(player.SightXParam ,player.InputHandler.SightDirection.x);
            player.Animator.SetFloat(player.SightYParam ,player.InputHandler.SightDirection.y);
            
            player.Weapon.Rotate(player.InputHandler.WorldSightPosition);

            if (player.InputHandler.MovementDirection.sqrMagnitude == 0)
            {
                stateMachine.SwitchState(PlayerStateType.Idle);
            }

            if (player.InputHandler.AttackInput && player.Weapon.IsReady)
            {
                stateMachine.SwitchState(PlayerStateType.Attack);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            _movement.SetVelocity(_movementSpeed, player.InputHandler.MovementDirection);
        }
    }
}