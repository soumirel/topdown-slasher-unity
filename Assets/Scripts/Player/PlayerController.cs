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

        public Core Core { get; private set; }
        public Animator Animator{ get; private set; }
        public PlayerInputHandler InputHandler { get; private set; }
        public AnimationEventHandler AnimationEventHandler { get; private set; }
        
        public PlayerData Data => _data;

        public readonly int SightXParam = Animator.StringToHash("sightX");
        public readonly int SightYParam = Animator.StringToHash("sightY");
        public readonly int MoveParam = Animator.StringToHash("move");
        public readonly int IdleParam = Animator.StringToHash("idle");
        public readonly int AimParam = Animator.StringToHash("aim");
        public readonly int AttackParam = Animator.StringToHash("attack");
        public readonly int HitParam = Animator.StringToHash("hit");
        public readonly int DeathParam = Animator.StringToHash("death");

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