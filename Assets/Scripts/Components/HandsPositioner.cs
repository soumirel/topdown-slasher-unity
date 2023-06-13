using System.Collections;
using ComponentSystem;
using Interfaces;
using UnityEngine;

namespace Components
{
    public class HandsPositioner : MonoBehaviour
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
        private Vector3 CalculateEulerRotation(float radiansAngle)
        {
            var rotation = Mathf.Atan2(-_ellipseTrajectory_a * Mathf.Cos(radiansAngle),
                _ellipseTrajectory_b * Mathf.Sin(radiansAngle));

            var eulerAngles = new Vector3(0f, 0f, rotation * Mathf.Rad2Deg + 90f);
            return eulerAngles;
        }


        // Line of sight constraint to constrain hand position on an ellipse
        private Vector2 ClampDirection(Vector2 direction)
        {
            var x = Mathf.Clamp(direction.x, _downClamp.x,
                _upClamp.x);
            var y = Mathf.Clamp(direction.y, _downClamp.y, _upClamp.y);
            
            return new Vector2(x, y);
        }

        
        public void ChangePosition(Vector2 direction)
        {
            direction = ClampDirection(direction);
            var radiansAngle = Mathf.Atan2(direction.y, direction.x);

            var currentPosition = _handTransform.localPosition;
            var targetPosition = CalculatePosition(radiansAngle);
            _handTransform.localPosition = Vector2.Lerp(currentPosition,
                targetPosition, Time.deltaTime * 20);

            var currentAngle = _handTransform.localRotation;
            var targetAngle = CalculateEulerRotation(radiansAngle);
            _handTransform.localRotation = Quaternion.Lerp(currentAngle, 
                Quaternion.Euler(targetAngle),
                Time.deltaTime * 20);
        }

        
        // public void Turn()
        // {
        //     var targetPosition = new Vector2(-_handTransform.localPosition.x, _handTransform.localPosition.y);
        //     var duration = _turnable.TurnSpeedSeconds;
        //     var startTime = Time.time;
        //     var startPosition = _handTransform.localPosition;
        //
        //     StartCoroutine(TurnCoroutine(startPosition, targetPosition, startTime, duration));
        // }
        //
        //
        // private IEnumerator TurnCoroutine(Vector2 startPosition, Vector2 targetPosition, float startTime, float duration)
        // {
        //     while (Time.time < startTime + duration)
        //     {
        //         var elapsedTime = Time.time - startTime;
        //         var t = elapsedTime / duration;
        //
        //         var newPosition = Vector2.Lerp(startPosition, targetPosition, t);
        //         _handTransform.localPosition = newPosition;
        //
        //         yield return null;
        //     }
        // }
    }
}