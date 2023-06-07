using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Input
{
    public class PlayerInputHandler : MonoBehaviour
    {
        private PlayerInput _playerInput;
        private Camera _camera;

        private Vector2 _rawInputMovement;
        private Vector2 _rawSightPosition;
        
        public bool AttackStarted { get; private set; }
        public bool AttackPerformed { get; private set; }
        public bool AttackCanceled { get; private set; }
        
        public bool AimPerformed { get; private set; }
        public bool AimCanceled { get; private set; }

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
            AttackStarted = value.started;
            AttackPerformed = value.performed;
            AttackCanceled = value.canceled;
        }

        public void OnSightPosition(InputAction.CallbackContext value)
        {
            _rawSightPosition = value.ReadValue<Vector2>();
            WorldSightPosition = (_camera.ScreenToWorldPoint(_rawSightPosition) -
                                   transform.position);
            SightDirection = WorldSightPosition.normalized;
        }

        public void OnAim(InputAction.CallbackContext value)
        {
            AimPerformed = value.performed;
            AimCanceled = value.canceled;
        }
    }
}