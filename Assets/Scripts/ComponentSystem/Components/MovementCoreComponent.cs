using System;
using UnityEngine;

namespace ComponentSystem
{
    public class MovementCoreComponent : CoreComponent
    {
        private Rigidbody2D _rb;

        public bool CanSetVelocity { get; set; }
        public Vector2 Velocity { get; private set; }

        private Vector2 _appliedVelocity;

        protected override void Awake()
        {
            base.Awake();
            _rb = GetComponentInParent<Rigidbody2D>();
            
            CanSetVelocity = true;
        }
        
        protected override void LogicUpdate()
        {
            Velocity = _rb.velocity;
        }
        
        #region Set Functions

        public void SetVelocityZero()
        {
            _appliedVelocity = Vector2.zero;        
            TryApplyVelocity();
        }

        public void SetVelocity(float velocity, Vector2 angle, int direction)
        {
            angle.Normalize();
            _appliedVelocity.Set(angle.x * velocity * direction, angle.y * velocity);
            TryApplyVelocity();
        }

        public void SetVelocity(float velocity, Vector2 direction)
        {
            _appliedVelocity = direction * velocity;
            TryApplyVelocity();
        }

        public void SetVelocityX(float velocity)
        {
            _appliedVelocity.Set(velocity, Velocity.y);
            TryApplyVelocity();
        }

        public void SetVelocityY(float velocity)
        {
            _appliedVelocity.Set(Velocity.x, velocity);
            TryApplyVelocity();
        }

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