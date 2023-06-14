using System;
using System.Collections.Generic;
using Animations;
using Components;
using Data;
using Interfaces;
using Player.Input;
using Player.PlayerFiniteStateMachine;
using UnityEngine;
using Weapons;


namespace Player
{
    public class Player : MonoBehaviour,
        IMovable, IHaveHands, ITurnable, IAnimated
    {
        [SerializeField] private PlayerDataSettings _dataSettings;

        public float MovementSpeed { get; set; }
        public int FacingDirection { get; set; }


        #region Components
        
        public PlayerInputHandler InputHandler { get; private set; }
        public Hand Hand { get; private set; }
        public Movement Movement { get; private set; }
        public PlayerStateMachine StateMachine { get; private set;  }
        public Rigidbody2D Rb { get; private set; }
        public Animator Animator { get; private set; }
        public AnyStateAnimator AnyStateAnimator { get; private set; }
        public Combat Combat { get; private set; }

        #endregion
        
        
        public void Awake()
        {
            InitializeData();
            
            InputHandler = GetComponent<PlayerInputHandler>();
            Rb = GetComponent<Rigidbody2D>();
            Movement = GetComponent<Movement>();
            Hand = GetComponentInChildren<Hand>();
            StateMachine = GetComponent<PlayerStateMachine>();
            AnyStateAnimator = GetComponent<AnyStateAnimator>();
            Animator = GetComponent<Animator>();
            Combat = GetComponent<Combat>();
            
            InitializeComponents();
        }

        
        private void InitializeData()
        {
            MovementSpeed = _dataSettings.MovementSpeed;
            FacingDirection = 1;
        }

        private void InitializeComponents()
        {
            Movement.Initialize(this);
            StateMachine.Initialize(this);
            AnyStateAnimator.Initialize(this);
        }
        
        public void Turn()
        {
            FacingDirection *= -1;
            transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y + 180f, 0f);
        }
    }
}