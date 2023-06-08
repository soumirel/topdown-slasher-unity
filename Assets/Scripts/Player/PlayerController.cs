using System;
using System.Collections;
using Components;
using Data;
using Player.Input;
using Player.PlayerFiniteStateMachine;
using UnityEngine;
using UnityEngine.InputSystem;
using Weapons;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerData _data;
        public PlayerData Data => _data;

        public Core Core { get; private set; }
        public Animator Animator{ get; private set; }
        public PlayerInputHandler InputHandler { get; private set; }
        public AnimationEventHandler AnimationEventHandler { get; private set; }

        public readonly int IDLE = Animator.StringToHash("idle");
        public readonly int MOVE = Animator.StringToHash("walk");
        public readonly int TURN = Animator.StringToHash("turn");
        public readonly int B_TURN = Animator.StringToHash("b_turn");

        private int _currentHashedAnimationName;

        public void Awake()
        {
            Core = GetComponentInChildren<Core>();
            Animator = GetComponent<Animator>();
            InputHandler = GetComponent<PlayerInputHandler>();
            AnimationEventHandler = GetComponent<AnimationEventHandler>();
            
            GetComponent<PlayerStateMachine>().InitializeStates();
        }
    }
}