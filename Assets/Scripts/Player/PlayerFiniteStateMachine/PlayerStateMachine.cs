
using System;
using System.Collections.Generic;
using Player.PlayerFiniteStateMachine.States;
using UnityEngine;

namespace Player.PlayerFiniteStateMachine
{
    public class PlayerStateMachine : MonoBehaviour
    {
        [SerializeField] private PlayerController _player;
        
        private Dictionary<PlayerStateType, PlayerState> _states
            = new Dictionary<PlayerStateType, PlayerState>();

        private PlayerState _currentState;

        public void Start()
        {
            if (_states.TryGetValue(PlayerStateType.Idle, out var startState))
            {
                _currentState = startState;
                _currentState.Enter();
            }
        }

        public void InitializeStates()
        {
           _states.Add(PlayerStateType.Idle, new IdleState(_player, this, _player.IdleParam));
           _states.Add(PlayerStateType.Move, new MoveState(_player, this, _player.MoveParam));
           _states.Add(PlayerStateType.Attack, new AttackState(_player, this, _player.AttackParam));
        }

        public void SwitchState(PlayerStateType stateType)
        {
            if (!_states.TryGetValue(stateType, out var newState)) return;
            
            _currentState.Exit();
            _currentState = newState;
            _currentState.Enter();
        }

        private void Update()
        {
            _currentState?.LogicUpdate();
        }

        private void FixedUpdate()
        {
            _currentState?.PhysicsUpdate();
        }
    }
}