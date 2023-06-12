using Components;
using ComponentSystem;
using UnityEngine;

namespace Player.PlayerFiniteStateMachine.States
{
    public class IdleState : PlayerState
    {
        protected Hands hands;
        protected Movement movement;

        public IdleState(Player player, PlayerStateMachine stateMachine, string animationTransitionParam)
            : base(player, stateMachine, animationTransitionParam)
        {
            hands = player.Hands;
            movement = player.Movement;
        }

        public override void Enter()
        {
            base.Enter();
            
            movement.Stop();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            hands.ChangePosition(player.InputHandler.SightDirection);
        }

        protected override void CheckTransitions()
        {
            if (player.FacingDirection != (int)Mathf.Sign(inputHandler.SightDirection.x))
            {
                SwitchState(PlayerStateType.Turn);
            }
            
            if (inputHandler.IsMoving)
            {
                SwitchState(PlayerStateType.Move);
            }
        }
    }
}