using ComponentSystem.Components;

namespace Player.PlayerFiniteStateMachine.States.SuperStates
{
    public class ControlledState : PlayerState
    {
        private HandsCoreComponent _handsCoreComponent;

        protected ControlledState(PlayerController player, PlayerStateMachine stateMachine, int hashedAnimatorParam)
            : base(player, stateMachine, hashedAnimatorParam)
        {
            _handsCoreComponent = player.Core.GetCoreComponent<HandsCoreComponent>();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            _handsCoreComponent.ChangePosition(
                player.InputHandler.SightDirection
            );
        }
    }
}