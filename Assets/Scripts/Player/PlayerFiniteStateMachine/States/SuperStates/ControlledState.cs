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
        }

        public override void Enter()
        {
            base.Enter();
            Visuals.OnTurnFinish += FinishTurn;
            _isTurning = false;
        }

        public override void Exit()
        {
            Visuals.OnTurnFinish -= FinishTurn;
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            ApplyTurnDirection();

            if (!_isTurning)
            {
                Hands?.ChangePosition(
                    player.InputHandler.SightDirection);
            }
        }

        private void ApplyTurnDirection()
        {
            if (core.CheckTurnNeed(inputHandler.SightDirection) && !_isTurning)
            {
                _isTurning = true;
                core.Turn();
            }
        }


        private void FinishTurn()
        {
            StartMainAnimation();
            _isTurning = false;
        }
    }
}