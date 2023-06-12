using Components;

namespace Player.PlayerFiniteStateMachine.States
{
    public class AttackState : PlayerState
    {
        private Combat _combat;
        
        public AttackState(Player player, PlayerStateMachine stateMachine, string animationTransitionParam) 
            : base(player, stateMachine, animationTransitionParam)
        {
            _combat = player.Combat;
        }

        public override void Enter()
        {
            base.Enter();
            _combat.Attack();
        }

        protected override void CheckTransitions()
        {
            
        }
    }
}