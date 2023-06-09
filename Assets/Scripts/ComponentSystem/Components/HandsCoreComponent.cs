using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace ComponentSystem.Components
{
    public class HandsCoreComponent : CoreComponent
    {
        [SerializeField] private Transform _handTransform;
        
        // TODO: Перенести настройку параметров
        [SerializeField] private Vector2 _upClamp;
        [SerializeField] private Vector2 _downClamp;

        [SerializeField] public float _ellipseTrajectory_a = 1.42f;
        [SerializeField] public float _ellipseTrajectory_b = 1.88f;
        
        private VisualsCoreComponent Visuals =>
            _visuals
                ? _visuals
                : core.GetCoreComponent(ref _visuals);
        private VisualsCoreComponent _visuals;


        protected override void Start()
        {
            base.Start();
            Visuals.OnTurnFinish += FinishTurn;
        }

        private void OnDisable()
        {
            Visuals.OnTurnFinish -= FinishTurn;
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
            return rotation * Mathf.Rad2Deg + 90f * core.FacingDirection;
        }


        // Line of sight constraint to constrain hand position on an ellipse
        private Vector2 ClampDirection(Vector2 direction)
        {
            var x = Mathf.Clamp(direction.x, _downClamp.x * core.FacingDirection,
                _upClamp.x * core.FacingDirection);
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

        public override void Turn()
        {
            var targetPosition = new Vector2(-_handTransform.localPosition.x,
                _handTransform.localPosition.y);
            StartCoroutine(TurnCoroutine(targetPosition));
        }

        private IEnumerator TurnCoroutine(Vector2 targetPosition)
        {
            while (targetPosition != (Vector2)_handTransform.localPosition)
            {
                Vector2 prevPosition = _handTransform.localPosition;
                var radiansAngle = Mathf.Atan2(targetPosition.y, targetPosition.x);

                var newPosition = Vector2.Lerp(prevPosition, 
                    CalculatePosition(radiansAngle), Time.deltaTime * 5f);
                _handTransform.localPosition = newPosition;
                yield return new WaitForEndOfFrame();
            }
        }

        private void FinishTurn()
        {
            StopAllCoroutines();
        }
    }
}