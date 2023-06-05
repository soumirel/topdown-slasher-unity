using UnityEngine;

namespace Player.PlayerFiniteStateMachine.States
{
    public class AttackState : PlayerState
    {
        public AttackState(PlayerController player, PlayerStateMachine stateMachine, int hashedAnimatorParam)
            : base(player, stateMachine, hashedAnimatorParam)
        {
            player.AnimationEventHandler.OnShotFrame += AnimationShotFrameTrigger;
        }
        
        public override void Enter()
        {
            base.Enter();
            _movement.SetVelocityZero();
        }

        protected override void AnimationFinishTrigger()
        {
            player.InputHandler.FinishAttack();
            stateMachine.SwitchState(PlayerStateType.Idle);
        }

        private void AnimationShotFrameTrigger()
        {
            player.Weapon.Use();
        }
    }
}