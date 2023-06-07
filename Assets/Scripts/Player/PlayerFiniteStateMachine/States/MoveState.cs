
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

        protected override void CheckTransitions()
        {
            base.CheckTransitions();
            
            if (player.InputHandler.IsMoving)
            {
                SwitchState(PlayerStateType.Idle);
            }

            if (player.InputHandler.AttackPerformed && combat.IsReady)
            {
                SwitchState(PlayerStateType.Aim);
            }
        }
        
        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            player.Animator.SetFloat(player.SightXParam ,player.InputHandler.SightDirection.x);
            player.Animator.SetFloat(player.SightYParam ,player.InputHandler.SightDirection.y);
            
            combat.CurrentWeapon.Rotate(player.InputHandler.WorldSightPosition);
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            movement.SetVelocity(_movementSpeed, player.InputHandler.MovementDirection);
        }
    }
}