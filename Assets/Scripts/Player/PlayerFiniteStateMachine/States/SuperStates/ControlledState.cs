using ComponentSystem.Components;
using UnityEngine;

namespace Player.PlayerFiniteStateMachine.States.SuperStates
{
    public class ControlledState : PlayerState
    {
        private HandsCoreComponent _handsCoreComponent;

        private bool _isTurning;

        private int i_start;
        private int i_finish;

        protected ControlledState(PlayerController player, PlayerStateMachine stateMachine, int hashedAnimatorParam)
            : base(player, stateMachine, hashedAnimatorParam)
        {
            _handsCoreComponent = player.Core.GetCoreComponent<HandsCoreComponent>();
        }

        public override void Enter()
        {
            base.Enter();
            player.AnimationEventHandler.OnTurnFinish += FinishTurn;
            _isTurning = false;
        }

        public override void Exit()
        {
            base.Exit();
            player.AnimationEventHandler.OnTurnFinish -= FinishTurn;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            ApplyTurnDirection();

            _handsCoreComponent.ChangePosition(
                player.InputHandler.SightDirection
            );
        }

        private void ApplyTurnDirection()
        {
            if (MovementCore.IfNeedTurn((int) Mathf.Sign(
                    player.InputHandler.SightDirection.x)) && !_isTurning)
            {
                _isTurning = true;
                player.Animator.SetBool(player.B_TURN, true);
            }
        }
        

        private void FinishTurn()
        {
            player.Animator.SetBool(player.B_TURN, false);
            MovementCore.Turn();
            _isTurning = false;
        }
    }
}