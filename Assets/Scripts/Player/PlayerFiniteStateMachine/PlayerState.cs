using Components;
using ComponentSystem;
using Player.Input;
using Player.PlayerFiniteStateMachine.States;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Player.PlayerFiniteStateMachine
{
    public abstract class PlayerState
    {
        protected readonly Player player;
        protected readonly PlayerInputHandler inputHandler;

        private readonly PlayerStateMachine _stateMachine;
        private readonly string _animationState;

        protected PlayerState(Player player, PlayerStateMachine stateMachine, string animationState)
        {
            this.player = player;
            _stateMachine = stateMachine;
            _animationState = animationState;
            
            inputHandler = this.player.InputHandler;
        }

        public virtual void Enter()
        {
            Debug.Log(this.GetType());
            StartMainAnimation();
        }

        protected void SwitchState(PlayerStateType type)
        {
            _stateMachine.SwitchState(type);
        }

        protected abstract void CheckTransitions();

        public virtual void LogicUpdate()
        {
            CheckTransitions();
        }

        public virtual void PhysicsUpdate() {}

        public virtual void Exit() {}

        protected void StartMainAnimation()
        {
            player.AnyStateAnimator.PlayAnimation(_animationState);
        }
    }
}