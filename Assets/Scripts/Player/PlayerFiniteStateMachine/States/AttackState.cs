namespace Player.PlayerFiniteStateMachine.States
{
    public class AttackState : PlayerState
    {
        public AttackState(Player player, PlayerStateMachine stateMachine, string animationTransitionParam) 
            : base(player, stateMachine, animationTransitionParam)
        {
        }

        protected override void CheckTransitions()
        {
            
        }
    }
}