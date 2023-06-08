using ComponentSystem;
using ComponentSystem.Components;
using Player.PlayerFiniteStateMachine.States;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Player.PlayerFiniteStateMachine
{
    public abstract class PlayerState
    {
        protected readonly PlayerController player;
        
        private readonly PlayerStateMachine _stateMachine;

        private readonly int _hashedAnimatorParam;

        protected MovementCoreComponent MovementCore;
        //protected CombatComponent combat;

        protected PlayerState(PlayerController player,
            PlayerStateMachine stateMachine,
            int hashedAnimatorParam)
        {
            this.player = player;
            
            _stateMachine = stateMachine;
            _hashedAnimatorParam = hashedAnimatorParam;
            
            MovementCore = player.Core.GetCoreComponent<MovementCoreComponent>();
            //combat = player.Core.GetCoreComponent<CombatComponent>();
        }

        public virtual void Enter()
        {
#if UNITY_EDITOR
            Debug.Log(this.GetType());
#endif
            player.Animator.SetBool(_hashedAnimatorParam, true);
            LogicUpdate();
            PhysicsUpdate();
        }

        protected void SwitchState(PlayerStateType type)
        {
            _stateMachine.SwitchState(type);
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
    }
}