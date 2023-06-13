
using System;
using System.Collections.Generic;
using Player.PlayerFiniteStateMachine.States;
using UnityEngine;

namespace Player.PlayerFiniteStateMachine
{
    public class PlayerStateMachine : MonoBehaviour
    {
        private Player _player;

        private Dictionary<PlayerStateType, PlayerState> _states;

        private PlayerState _currentState;

        public void Initialize(Player player)
        {
            _player = player;
            InitializeStates();
        }
        
        private void InitializeStates()
        {
            _states.TryAdd(PlayerStateType.Idle, new IdleState(_player, this, "player_idle"));
            _states.TryAdd(PlayerStateType.Move, new MoveState(_player, this, "player_move"));
        }

        public void Awake()
        {
            _states = new Dictionary<PlayerStateType, PlayerState>();
        }

        public void Start()
        {
            if (_states.TryGetValue(PlayerStateType.Idle, out var startState))
            {
                _currentState = startState;
                _currentState.Enter();
            }
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