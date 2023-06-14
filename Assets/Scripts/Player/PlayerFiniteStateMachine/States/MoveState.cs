using Components;
using UnityEngine;
using Weapons;

namespace Player.PlayerFiniteStateMachine.States
{
    public class MoveState : PlayerState
    {
        protected Movement movement;
        private Hand _hand;
        
        public MoveState(Player player, PlayerStateMachine stateMachine, string animationTransitionParam) 
            : base(player, stateMachine, animationTransitionParam)
        {
            movement = player.Movement;
            _hand = player.Hand;
        }
        
        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            _hand.FollowSight(inputHandler.SightDirection);
            
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