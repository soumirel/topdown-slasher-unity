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

        }

        private void AnimationShotFrameTrigger()
        {
        }
    }
}