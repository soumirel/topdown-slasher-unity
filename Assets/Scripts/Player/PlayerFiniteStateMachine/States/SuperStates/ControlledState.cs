using Components;
using ComponentSystem;
using UnityEngine;

namespace Player.PlayerFiniteStateMachine.States.SuperStates
{
    public class ControlledState : PlayerState
    {
        protected Hands hands;
        protected Movement movement;

        protected ControlledState(Player player, PlayerStateMachine stateMachine, string animationTransitionParam)
            : base(player, stateMachine, animationTransitionParam)
        {
            movement = player.Movement;
            hands = player.Hands;
            
        }

        public override void Enter()
        {
            base.Enter();
            player.OnTurnFinish += FinishTurn;
        }

        public override void Exit()
        {
            player.OnTurnFinish -= FinishTurn;
            base.Exit();
        }

        protected override void CheckTransitions() { }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            ApplyTurnDirection();
            
            if (!player.IsTurning)
            {
                hands.ChangePosition(player.InputHandler.SightDirection);
            }
        }

        private void ApplyTurnDirection()
        {
            if (player.FacingDirection != (int)Mathf.Sign(inputHandler.SightDirection.x))
            {
                player.StartTurn();
            }
        }


        private void FinishTurn()
        {
            Debug.Log(nameof(FinishTurn));
            StartMainAnimation();
        }
    }
}