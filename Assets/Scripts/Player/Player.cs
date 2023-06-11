using Components;
using Data;
using Interfaces;
using Player.Input;
using Player.PlayerFiniteStateMachine;
using UnityEngine;


namespace Player
{
    public class Player : MonoBehaviour,
        IMovable, IHaveHands, ITurnable
    {
        [SerializeField] private PlayerDataSettings _dataSettings;
        public float MovementSpeed { get; set; }
        public float TurnSpeedSeconds { get; set; }
        

        public bool IsTurning { get; set; }
        public int FacingDirection { get; set; }
        
        
        #region Components
        
        public PlayerInputHandler InputHandler { get; private set; }
        public PlayerVisuals PlayerVisuals { get; private set; }
        public Hands Hands { get; private set; }
        public Movement Movement { get; private set; }
        public PlayerStateMachine StateMachine { get; private set;  }
        public Rigidbody2D Rb { get; set; }

        #endregion
        
        
        public void Awake()
        {
            InitializeData();
            
            InputHandler = GetComponent<PlayerInputHandler>();
            PlayerVisuals = GetComponent<PlayerVisuals>();
            Rb = GetComponent<Rigidbody2D>();
            Movement = GetComponent<Movement>();
            Hands = GetComponentInChildren<Hands>();
            StateMachine = GetComponent<PlayerStateMachine>();
            
            InitializeComponents();
        }

        public void OnEnable()
        {
            PlayerVisuals.OnTurnFinish += FinishTurn;
        }

        private void OnDisable()
        {
            PlayerVisuals.OnTurnFinish -= FinishTurn;
        }


        private void InitializeData()
        {
            MovementSpeed = _dataSettings.MovementSpeed;
            TurnSpeedSeconds = _dataSettings.TurnSpeed;
            IsTurning = false;
            FacingDirection = 1;
        }

        private void InitializeComponents()
        {
            Movement.Initialize(this);
            PlayerVisuals.Initialize(this);
            StateMachine.Initialize(this);
            Hands.Initialize(this);
        }
        
        
        public void StartTurn()
        {
            if (!IsTurning)
            {
                IsTurning = true;
                FacingDirection *= -1;
            
                PlayerVisuals.Turn();
                Hands.Turn();
            }
            
        }

        public void FinishTurn()
        {
            IsTurning = false;
        }
    }
}