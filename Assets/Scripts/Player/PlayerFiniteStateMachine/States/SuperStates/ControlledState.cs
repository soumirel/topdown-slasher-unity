using ComponentSystem;
using ComponentSystem.Components;
using UnityEngine;

namespace Player.PlayerFiniteStateMachine.States.SuperStates
{
    public class ControlledState : PlayerState
    {
        protected HandsCoreComponent Hands =>
            _handsCoreComponent
                ? _handsCoreComponent
                : core.GetCoreComponent(ref _handsCoreComponent);

        protected MovementCoreComponent Movement =>
            _movement ? _movement : core.GetCoreComponent(ref _movement);


        private HandsCoreComponent _handsCoreComponent;
        private MovementCoreComponent _movement;

        private bool _isTurning;

        protected ControlledState(PlayerController player, PlayerStateMachine stateMachine, int hashedAnimatorParam)
            : base(player, stateMachine, hashedAnimatorParam)
        {
            _handsCoreComponent = player.Core.GetCoreComponent<HandsCoreComponent>();
        }

        public override void Enter()
        {
            base.Enter();
            animationEventHandler.OnTurnFinish += FinishTurn;
            _isTurning = false;
        }

        public override void Exit()
        {
            base.Exit();
            animationEventHandler.OnTurnFinish -= FinishTurn;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            ApplyTurnDirection();

            Hands?.ChangePosition(
                player.InputHandler.SightDirection
            );
        }

        private void ApplyTurnDirection()
        {
            if ((bool) Movement?.IfNeedTurn((int)Mathf.Sign(
                    player.InputHandler.SightDirection.x)) && !_isTurning)
            {
                _isTurning = true;
                animator.SetBool(player.B_TURN, true);
            }
        }


        private void FinishTurn()
        {
            animator.SetBool(player.B_TURN, false);
            Movement?.Turn();
            _isTurning = false;
        }
    }
}