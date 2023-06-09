﻿using UnityEngine;
using UnityEngine.Serialization;

namespace ComponentSystem.Components
{
    public class HandsCoreComponent : CoreComponent
    {
        [SerializeField] private Transform _handTransform;


        // TODO: Перенести настройку параметров
        [SerializeField] public float _maxDirectionX = 0.7f;
        [SerializeField] public float _minDirectionX = 0.7f;
        [SerializeField] public float _maxDirectionY = 0.7f;
        [SerializeField] public float _minDirectionY = 0.7f;

        [SerializeField] public float _ellipseTrajectory_a = 2;
        [SerializeField] public float _ellipseTrajectory_b = 1.4f;


        // Calculating position from the parametric equation of an ellipse
        private Vector2 CalculatePosition(float radiansAngle)
        {
            var x = _ellipseTrajectory_a * Mathf.Cos(radiansAngle);
            var y = _ellipseTrajectory_b * Mathf.Sin(radiansAngle);
            return new Vector2(x, y);
        }


        // Calculation of the slope coefficient of a tangent to a point on an ellipse
        private float CalculateRotation(float radiansAngle)
        {
            var rotation = Mathf.Atan2(-_ellipseTrajectory_a * Mathf.Cos(radiansAngle),
                _ellipseTrajectory_b * Mathf.Sin(radiansAngle));
            return rotation * Mathf.Rad2Deg;
        }


        // Line of sight constraint to constrain hand position on an ellipse
        private Vector2 ClampDirection(Vector2 direction)
        {
            var x = Mathf.Clamp(direction.x, _minDirectionX, _maxDirectionX);
            var y = Mathf.Clamp(direction.y, _minDirectionY, _maxDirectionY);
            return new Vector2(x, y);
        }

        public void ChangePosition(Vector2 direction)
        {
            direction = ClampDirection(direction);
            var radiansAngle = Mathf.Atan2(direction.y, direction.x);

            var oldPosition = _handTransform.localPosition;
            var newPosition = CalculatePosition(radiansAngle);
            _handTransform.localPosition = Vector2.Lerp(oldPosition, newPosition, Time.deltaTime * 20);

            var oldAngle = _handTransform.localRotation;
            var rotationAngle = CalculateRotation(radiansAngle);
            _handTransform.localRotation = Quaternion.Lerp(oldAngle, Quaternion.Euler(0, 0, rotationAngle + 90f),
                Time.deltaTime * 20);
        }

        public void Turn(Vector2 direction)
        {
            var radiansAngle = Mathf.Atan2(direction.y, direction.x);
            var oldPosition = _handTransform.localPosition;
            var newPosition = Vector2.Lerp(oldPosition, CalculatePosition(radiansAngle), Time.deltaTime * 10);
            _handTransform.localPosition = newPosition;
        }
    }
}