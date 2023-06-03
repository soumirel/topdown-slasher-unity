using Player.PlayerFiniteStateMachine.States;

namespace Player.PlayerFiniteStateMachine
{
    public class AttackState : PlayerState
    {
        public AttackState(PlayerController player, PlayerStateMachine stateMachine, int hashedAnimatorParam) 
            : base(player, stateMachine, hashedAnimatorParam) {}
        

        protected override void AnimationFinishTrigger()
        {
            player.InputHandler.FinishAttack();
            stateMachine.SwitchState(PlayerStateType.Idle);
        }

    }
}