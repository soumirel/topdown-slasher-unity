using Components;
using ComponentSystem;
using UnityEngine;

namespace Player.PlayerFiniteStateMachine.States
{
    public class IdleState : PlayerState
    {
        private HandsPositioner _handsPositioner;
        private Movement _movement;
        private Combat _combat;

        public IdleState(Player player, PlayerStateMachine stateMachine, string animationTransitionParam)
            : base(player, stateMachine, animationTransitionParam)
        {
            _handsPositioner = player.HandsPositioner;
            _movement = player.Movement;
            _combat = player.Combat;
        }

        public override void Enter()
        {
            base.Enter();
            inputHandler.OnAttackAction += Attack;
            _movement.Stop();
        }

        public override void Exit()
        {
            inputHandler.OnAttackAction -= Attack;
            base.Exit();
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


        private void Attack()
        {
            _combat.Attack();
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