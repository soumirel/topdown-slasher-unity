using Components;
using ComponentSystem.Components;
using UnityEngine;

namespace Player.PlayerFiniteStateMachine.States
{
    public class AttackState : PlayerState
    {
        public AttackState(PlayerController player, PlayerStateMachine stateMachine, int hashedAnimatorParam)
            : base(player, stateMachine, hashedAnimatorParam)
        {
            player.AnimationEventHandler.OnShotFrame += AnimationShotFrameTrigger;
            combat = player.Core.GetCoreComponent<CombatComponent>();
        }

        protected override void AnimationFinishTrigger()
        {
            if (player.InputHandler.AttackCanceled)
            {
                SwitchState(player.InputHandler.IsMoving ? PlayerStateType.Move : PlayerStateType.Idle);
            }
            else
            {
                SwitchState(PlayerStateType.Aim);
            }
        }

        private void AnimationShotFrameTrigger()
        {
            player.Animator.SetTrigger(player.AttackParam);
            combat.Attack();
        }
    }
}