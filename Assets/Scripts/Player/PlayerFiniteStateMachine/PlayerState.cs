using ComponentSystem;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Player.PlayerFiniteStateMachine
{
    public abstract class PlayerState
    {
        protected readonly PlayerController player;
        protected readonly PlayerStateMachine stateMachine;

        private readonly int _hashedAnimatorParam;
        
        protected MovementComponent _movement;

        protected PlayerState(PlayerController player, PlayerStateMachine stateMachine, int hashedAnimatorParam)
        {
            this.player = player;
            this.stateMachine = stateMachine;
            _hashedAnimatorParam = hashedAnimatorParam;
            player.AnimationEventHandler.OnAnimationFinish += AnimationFinishTrigger;
            
            _movement = player.Core.GetCoreComponent<MovementComponent>();
        }

        public virtual void Enter()
        {
            player.Animator.SetBool(_hashedAnimatorParam, true);
            LogicUpdate();
            PhysicsUpdate();
            
        }
        
        public virtual void LogicUpdate() {}
        public virtual void PhysicsUpdate() {}

        public virtual void Exit()
        {
            player.Animator.SetBool(_hashedAnimatorParam, false);
        }
        
        protected virtual void AnimationStartTrigger() {}
        protected virtual void AnimationFinishTrigger() {}
    }
}