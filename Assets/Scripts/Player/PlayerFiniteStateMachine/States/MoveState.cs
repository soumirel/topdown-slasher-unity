using Components;
using UnityEngine;

namespace Player.PlayerFiniteStateMachine.States
{
    public class MoveState : PlayerState
    {
        protected Movement movement;
        private HandsPositioner _handsPositioner;
        
        public MoveState(Player player, PlayerStateMachine stateMachine, string animationTransitionParam) 
            : base(player, stateMachine, animationTransitionParam)
        {
            movement = player.Movement;
            _handsPositioner = player.HandsPositioner;
        }
        
        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            _handsPositioner.ChangePosition(player.InputHandler.SightDirection);
        }

        protected override void CheckTransitions()
        {
            if (player.FacingDirection != (int)Mathf.Sign(inputHandler.SightDirection.x))
            {
                SwitchState(PlayerStateType.Turn);
            }
            
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