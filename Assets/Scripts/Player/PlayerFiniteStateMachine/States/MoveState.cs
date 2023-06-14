using Components;
using UnityEngine;

namespace Player.PlayerFiniteStateMachine.States
{
    public class MoveState : PlayerState
    {
        protected Movement movement;
        private HandsPositioner _handsPositioner;
        
        public MoveState(Player player, PlayerStateMachine stateMachine, string animationState) 
            : base(player, stateMachine, animationState)
        {
            movement = player.Movement;
            _handsPositioner = player.HandsPositioner;
        }
        
        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            _handsPositioner.ChangePosition(inputHandler.SightDirection);
            
            if (player.FacingDirection != (int)Mathf.Sign(inputHandler.SightDirection.x))
            {
                player.Turn();
            }
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