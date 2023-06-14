using System;
using UnityEngine;
using Weapons;

namespace Components
{
    public class Hand : MonoBehaviour
    {
        [Header("Sight direction clamping")]
        [SerializeField] private Vector2 _upClampSightDirection;
        [SerializeField] private Vector2 _downClampSightDirection;

        [Header("Ellipse parameters")]
        
        [Tooltip("Semi-major axis of ellipse")] 
        [Range(0f, 5f)] 
        [SerializeField]
        private float _ellipseWidth = 1.42f;

        [Tooltip("Semi-minor axis of ellipse")] 
        [Range(0f, 5f)] 
        [SerializeField]
        private float _ellipseHeihgt = 1.88f;
        
        private bool _canUse;
        
        private Weapon _weapon;

        
        private void OnEnable()
        {
            _canUse = true;
        }
        

        public void HandleWeapon(Weapon weapon)
        {
            if (_weapon)
            {
                _weapon.OnUseAvailable -= AllowUsage;
            }
            
            _weapon = weapon;
            _weapon.OnUseAvailable += AllowUsage;
        }

        
        public void UseWeapon()
        {
            if (_canUse)
            {
                _weapon.Use();
                ProhibitUsage();
            }
        }

        
        private void AllowUsage()
        {
            _canUse = true;
        }


        private void ProhibitUsage()
        {
            _canUse = false;
        }
        

        public void FollowSight(Vector2 sightDirection)
        {
            sightDirection = ClampDirection(sightDirection);
            var sightRadians = Mathf.Atan2(sightDirection.y, sightDirection.x);

            SetPosition(sightRadians);

            SetRotation(sightRadians);
        }


        private void SetPosition(float sightRadians)
        {
            var currentPosition = transform.localPosition;
            var targetPosition = CalculatePosition(sightRadians);
            transform.localPosition = Vector2.Lerp
            (
                currentPosition,
                targetPosition, Time.deltaTime * 20
            );
        }

        private void SetRotation(float sightRadians)
        {
            var currentAngle = transform.localRotation;
            var targetAngle = CalculateEulerRotation(sightRadians);
            transform.localRotation = Quaternion.Lerp
            (
                currentAngle,
                Quaternion.Euler(targetAngle),
                Time.deltaTime * 20
            );
        }


        // Calculating position from the parametric equation of an ellipse
        private Vector2 CalculatePosition(float radiansAngle)
        {
            var x = _ellipseWidth * Mathf.Cos(radiansAngle);
            var y = _ellipseHeihgt * Mathf.Sin(radiansAngle);
            return new Vector2(x, y);
        }


        // Calculation of the slope coefficient of a tangent to a point on an ellipse
        private Vector3 CalculateEulerRotation(float radiansAngle)
        {
            var rotation = Mathf.Atan2
            (
                -_ellipseWidth * Mathf.Cos(radiansAngle),
                _ellipseHeihgt * Mathf.Sin(radiansAngle)
            );

            var eulerAngles = new Vector3(0f, 0f, rotation * Mathf.Rad2Deg + 90f);
            return eulerAngles;
        }


        // Clamp sight direction to restrict elliptical movement
        private Vector2 ClampDirection(Vector2 direction)
        {
            var x = Mathf.Clamp(direction.x, _downClampSightDirection.x, _upClampSightDirection.x);
            var y = Mathf.Clamp(direction.y, _downClampSightDirection.y, _upClampSightDirection.y);

            return new Vector2(x, y);
        }
    }
}