using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Input
{
    public class PlayerInputHandler : MonoBehaviour
    {
        public event Action OnAttackAction;
        
        private PlayerInput _playerInput;
        private Camera _camera;

        private Vector2 _rawInputMovement;
        private Vector2 _rawSightPosition;
        
        public bool AttackStarted { get; private set; }

        public bool IsMoving { get; private set; }
        public Vector2 MovementDirection { get; private set; }
        public Vector2 WorldSightPosition { get; private set; }
        public Vector2 SightDirection { get; private set; }
        

        private void Start()
        {
            _playerInput = GetComponent<PlayerInput>();
            _camera = Camera.main;
        }

        public void OnMovement(InputAction.CallbackContext value)
        {
            Vector2 inputMovement = value.ReadValue<Vector2>();
            _rawInputMovement = new Vector3(inputMovement.x, inputMovement.y, 0);
            MovementDirection = _rawInputMovement.normalized;
            IsMoving = MovementDirection.sqrMagnitude != 0;
        }

        public void OnAttack(InputAction.CallbackContext value)
        {
            if (value.started)
            {
                OnAttackAction?.Invoke();
            }
        }

        public void OnSightPosition(InputAction.CallbackContext value)
        {
            _rawSightPosition = value.ReadValue<Vector2>();
            WorldSightPosition = (_camera.ScreenToWorldPoint(_rawSightPosition) -
                                   transform.position);
            SightDirection = WorldSightPosition.normalized;
        }
    }
}