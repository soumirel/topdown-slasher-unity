using System.Collections;
using ComponentSystem;
using Interfaces;
using UnityEngine;

namespace Components
{
    public class Hands : MonoBehaviour
    {
        [SerializeField] private Transform _handTransform;
        
        [SerializeField] private Vector2 _upClamp;
        [SerializeField] private Vector2 _downClamp;

        [SerializeField] public float _ellipseTrajectory_a = 1.42f;
        [SerializeField] public float _ellipseTrajectory_b = 1.88f;

        private ITurnable _turnable;

        
        public void Initialize(ITurnable turnable)
        {
            _turnable = turnable;
        }


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
            
            return rotation * Mathf.Rad2Deg + 90f * _turnable.FacingDirection;
        }


        // Line of sight constraint to constrain hand position on an ellipse
        private Vector2 ClampDirection(Vector2 direction)
        {
            var x = Mathf.Clamp(direction.x, _downClamp.x * _turnable.FacingDirection,
                _upClamp.x * _turnable.FacingDirection);
            var y = Mathf.Clamp(direction.y, _downClamp.y, _upClamp.y);
            
           
            return new Vector2(x, y);
        }

        
        public void ChangePosition(Vector2 direction)
        {
            direction = ClampDirection(direction);
            var radiansAngle = Mathf.Atan2(direction.y, direction.x);

            var oldPosition = _handTransform.localPosition;
            var newPosition = CalculatePosition(radiansAngle);
            _handTransform.localPosition = Vector2.Lerp(oldPosition,
                newPosition, Time.deltaTime * 20);

            var oldAngle = _handTransform.localRotation;
            var rotationAngle = CalculateRotation(radiansAngle);
            _handTransform.localRotation = Quaternion.Lerp(oldAngle,
                Quaternion.Euler(0, 0, rotationAngle),
                Time.deltaTime * 20);
        }

        
        public void Turn()
        {
            var targetPosition = new Vector2(-_handTransform.localPosition.x, _handTransform.localPosition.y);
            var duration = _turnable.TurnSpeedSeconds;
            var startTime = Time.time;
            var startPosition = _handTransform.localPosition;

            StartCoroutine(TurnCoroutine(startPosition, targetPosition, startTime, duration));
        }

        
        private IEnumerator TurnCoroutine(Vector2 startPosition, Vector2 targetPosition, float startTime, float duration)
        {
            while (Time.time < startTime + duration)
            {
                var elapsedTime = Time.time - startTime;
                var t = elapsedTime / duration;

                var newPosition = Vector2.Lerp(startPosition, targetPosition, t);
                _handTransform.localPosition = newPosition;

                var radiansAngle = Mathf.Atan2(targetPosition.y, targetPosition.x);
                var rotationAngle = CalculateRotation(radiansAngle);
                _handTransform.localRotation = Quaternion.Euler(0, 0, rotationAngle);

                yield return null;
            }

            // Устанавливаем конечную позицию и завершаем анимацию
            _handTransform.localPosition = targetPosition;
        }
    }
}