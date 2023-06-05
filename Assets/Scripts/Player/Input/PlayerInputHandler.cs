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
        private bool _attackInput;

        private Vector2 _movementDirection;
        private Vector2 _worldSightPosition;
        private Vector2 _sightDirection;
        private float _sightRotation;

        public Vector2 WorldSightPosition => _worldSightPosition;
        public bool AttackInput => _attackInput;
        public Vector2 SightDirection => _sightDirection;
        public Vector2 MovementDirection => _movementDirection;



        private void Start()
        {
            _playerInput = GetComponent<PlayerInput>();
            _camera = Camera.main;
        }

        public void OnMovement(InputAction.CallbackContext value)
        {
            Vector2 inputMovement = value.ReadValue<Vector2>();
            _rawInputMovement = new Vector3(inputMovement.x, inputMovement.y, 0);
            _movementDirection = _rawInputMovement.normalized;
        }

        public void OnAttack(InputAction.CallbackContext value)
        {
            if (value.started)
            {
                _attackInput = true;
            }
        }

        public void OnSightPosition(InputAction.CallbackContext value)
        {
            _rawSightPosition = value.ReadValue<Vector2>();
            _worldSightPosition = (_camera.ScreenToWorldPoint(_rawSightPosition) -
                                   transform.position);
            _sightDirection = _worldSightPosition.normalized;
            
        }

        public void FinishAttack()
        {
            _attackInput = false;
        }
    }
}