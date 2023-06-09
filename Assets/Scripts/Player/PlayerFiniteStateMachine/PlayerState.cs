using Components;
using ComponentSystem;
using ComponentSystem.Components;
using Player.Input;
using Player.PlayerFiniteStateMachine.States;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Player.PlayerFiniteStateMachine
{
    public abstract class PlayerState
    {
        protected readonly PlayerController player;
        protected readonly Core core;
        protected readonly PlayerInputHandler inputHandler;
        protected readonly Animator animator;
        protected readonly AnimationEventHandler animationEventHandler;
        
        private readonly PlayerStateMachine _stateMachine;
        private readonly int _hashedAnimatorParam;

        protected PlayerState(PlayerController player,
            PlayerStateMachine stateMachine,
            int hashedAnimatorParam)
        {
            this.player = player;
            _stateMachine = stateMachine;
            _hashedAnimatorParam = hashedAnimatorParam;

            core = this.player.Core;
            inputHandler = this.player.InputHandler;
            animator = this.player.Animator;
            animationEventHandler = this.player.AnimationEventHandler;
        }

        public virtual void Enter()
        {
#if UNITY_EDITOR
            Debug.Log(this.GetType());
#endif
            StartMainAnimation();
            LogicUpdate();
            PhysicsUpdate();
        }

        protected void SwitchState(PlayerStateType type)
        {
            _stateMachine.SwitchState(type);
        }

        protected virtual void CheckTransitions() {}

        public virtual void LogicUpdate()
        {
            CheckTransitions();
        }

        public virtual void PhysicsUpdate() {}

        public virtual void Exit()
        {
            StopMainAnimation();
        }

        protected void StartMainAnimation()
        {
            animator.SetBool(_hashedAnimatorParam, true);
        }

        protected void StopMainAnimation()
        {
            animator.SetBool(_hashedAnimatorParam, false);
        }
    }
}