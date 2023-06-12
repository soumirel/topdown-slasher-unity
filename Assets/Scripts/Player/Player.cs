using System;
using System.Collections.Generic;
using Animations;
using Components;
using Data;
using Interfaces;
using Player.Input;
using Player.PlayerFiniteStateMachine;
using UnityEngine;


namespace Player
{
    public class Player : MonoBehaviour,
        IMovable, IHaveHands, ITurnable, IAnimated
    {
        [SerializeField] private PlayerDataSettings _dataSettings;
        public float MovementSpeed { get; set; }
        public float TurnSpeedSeconds { get; set; }
        

        public bool IsTurning { get; set; }
        public int FacingDirection { get; set; }
        public List<string> AnimationTransitionTags { get; private set; }
        
        
        #region Components
        
        public PlayerInputHandler InputHandler { get; private set; }
        public Hands Hands { get; private set; }
        public Movement Movement { get; private set; }
        public PlayerStateMachine StateMachine { get; private set;  }
        public Rigidbody2D Rb { get; private set; }
        public Animator Animator { get; private set; }
        public SpriteRenderer SpriteRenderer { get; private set;}
        public AnyStateAnimator AnyStateAnimator { get; private set; }

        #endregion
        
        
        public void Awake()
        {
            InitializeData();
            
            InputHandler = GetComponent<PlayerInputHandler>();
            Rb = GetComponent<Rigidbody2D>();
            Movement = GetComponent<Movement>();
            Hands = GetComponentInChildren<Hands>();
            StateMachine = GetComponent<PlayerStateMachine>();
            AnyStateAnimator = GetComponent<AnyStateAnimator>();
            SpriteRenderer = GetComponent<SpriteRenderer>();
            Animator = GetComponent<Animator>();
            
            InitializeComponents();
        }

        
        private void InitializeData()
        {
            MovementSpeed = _dataSettings.MovementSpeed;
            TurnSpeedSeconds = _dataSettings.TurnSpeed;
            IsTurning = false;
            FacingDirection = 1;

            AnimationTransitionTags = new List<string>
            {
                "idle",
                "move",
                "turn",
                "hit",
            };
        }

        private void InitializeComponents()
        {
            Movement.Initialize(this);
            StateMachine.Initialize(this);
            Hands.Initialize(this);
            AnyStateAnimator.Initialize(this);
        }
    }
}