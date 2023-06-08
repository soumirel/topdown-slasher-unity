using System;
using UnityEngine;

namespace ComponentSystem
{
    public class MovementCoreComponent : CoreComponent
    {
        private Rigidbody2D _rb;

        public bool CanSetVelocity { get; set; }
        public Vector2 Velocity { get; private set; }
        public int FacingDirection { get; private set; }
        
        private Vector2 _appliedVelocity;

        public void Awake()
        {
            _rb = GetComponentInParent<Rigidbody2D>();
            
            CanSetVelocity = true;
            FacingDirection = 1;
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
        
        public bool IfNeedTurn(int xDirection)
        {
            return xDirection != FacingDirection;
        }

        public void Turn()
        {
            FacingDirection *= -1;
            _rb.transform.Rotate(0.0f, 180.0f, 0.0f);
        }
        

        #endregion
    }
}