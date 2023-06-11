using System;
using ComponentSystem;
using Interfaces;
using UnityEngine;

namespace Components
{
    public class Movement : MonoBehaviour
    {
        private IMovable _movable;
        private Rigidbody2D _rb;

        public bool CanSetVelocity { get; set; }
        public Vector2 Velocity { get; private set; }

        private Vector2 _appliedVelocity;

        public void Initialize(IMovable movable)
        {
            _movable = movable;
            _rb = movable.Rb;
        }

        protected void OnEnable()
        {
            CanSetVelocity = true;
        }

        protected void OnDisable()
        {
            CanSetVelocity = false;
        }

        protected void Update()
        {
            Velocity = _rb.velocity;
        }
        
        #region Setters

        public void Move(Vector2 direction)
        {
            _appliedVelocity = direction * _movable.MovementSpeed;
            TryApplyVelocity();
        }

        public void Stop()
        {
            _appliedVelocity = Vector2.zero;        
            TryApplyVelocity();
        }
        
        
        // public void SetVelocity(float velocity, Vector2 angle, int direction)
        // {
        //     angle.Normalize();
        //     _appliedVelocity.Set(angle.x * velocity * direction, angle.y * velocity);
        //     TryApplyVelocity();
        // }
        //
        // public void SetVelocity(float velocity, Vector2 direction)
        // {
        //     _appliedVelocity = direction * velocity;
        //     TryApplyVelocity();
        // }
        //
        // public void SetVelocityX(float velocity)
        // {
        //     _appliedVelocity.Set(velocity, Velocity.y);
        //     TryApplyVelocity();
        // }
        //
        // public void SetVelocityY(float velocity)
        // {
        //     _appliedVelocity.Set(Velocity.x, velocity);
        //     TryApplyVelocity();
        // }
        //
        
        
        private void TryApplyVelocity()
        {
            if (CanSetVelocity)
            {
                _rb.velocity = _appliedVelocity;
                Velocity = _appliedVelocity;
            }        
        }
        
        #endregion
    }
}