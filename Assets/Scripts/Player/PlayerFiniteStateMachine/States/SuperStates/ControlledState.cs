using Components;
using ComponentSystem;
using UnityEngine;

namespace Player.PlayerFiniteStateMachine.States.SuperStates
{
    public class ControlledState : PlayerState
    {
        protected Hands hands;
        protected Movement movement;
        protected PlayerVisuals visuals;

        protected ControlledState(Player player, PlayerStateMachine stateMachine, int hashedAnimatorParam)
            : base(player, stateMachine, hashedAnimatorParam)
        {
            movement = player.Movement;
            visuals = player.PlayerVisuals;
            hands = player.Hands;
            
        }

        public override void Enter()
        {
            base.Enter();
            visuals.OnTurnFinish += FinishTurn;
        }

        public override void Exit()
        {
            visuals.OnTurnFinish -= FinishTurn;
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
            StartMainAnimation();
        }
    }
}