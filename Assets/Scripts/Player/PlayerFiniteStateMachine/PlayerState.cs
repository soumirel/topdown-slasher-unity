using ComponentSystem;
using ComponentSystem.Components;
using Player.PlayerFiniteStateMachine.States;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Player.PlayerFiniteStateMachine
{
    public abstract class PlayerState
    {
        protected readonly PlayerController player;
        
        private readonly PlayerStateMachine stateMachine;
        private readonly int _hashedAnimatorParam;

        protected MovementComponent movement;
        protected CombatComponent combat;

        protected PlayerState(PlayerController player, PlayerStateMachine stateMachine, int hashedAnimatorParam)
        {
            this.player = player;
            this.stateMachine = stateMachine;
            _hashedAnimatorParam = hashedAnimatorParam;
            player.AnimationEventHandler.OnAnimationFinish += AnimationFinishTrigger;

            movement = player.Core.GetCoreComponent<MovementComponent>();
            combat = player.Core.GetCoreComponent<CombatComponent>();
        }

        public virtual void Enter()
        {
            StartAnimation();
            LogicUpdate();
            PhysicsUpdate();
        }

        protected void SwitchState(PlayerStateType type)
        {
            stateMachine.SwitchState(type);
        }

        protected virtual void CheckTransitions()
        {
            
        }

        public virtual void LogicUpdate()
        {
            CheckTransitions();
        }

        public virtual void PhysicsUpdate()
        {
        }

        public virtual void Exit()
        {
            player.Animator.SetBool(_hashedAnimatorParam, false);
        }

        protected virtual void StartAnimation()
        {
            player.Animator.SetBool(_hashedAnimatorParam, true);
        }

        protected virtual void AnimationStartTrigger()
        {
        }

        protected virtual void AnimationFinishTrigger()
        {
        }
    }
}