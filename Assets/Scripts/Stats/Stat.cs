using System;
using UnityEngine;

namespace ComponentSystem.Stats
{
    public class Stat
    {
        public event Action OnMinValue;

        private float _value;
        private float _minValue;
        private float _maxValue;

        public Stat(float maxValue, float minValue = 0)
        {
            _minValue = Mathf.Min(minValue, maxValue);
            _maxValue = Mathf.Max(minValue, maxValue);

            _value = _maxValue;
        }


        public float Value
        {
            set
            {
                _value = Mathf.Clamp(value, _minValue, _maxValue);
                if (_value <= _minValue)
                {
                    OnMinValue?.Invoke();
                }
            }
            get => _value;
        }

        public float MinValue
        {
            set => _minValue = Mathf.Min(value, _maxValue);
            get => _minValue;
        }

        public float MaxValue
        {
            set => _maxValue = Mathf.Max(value, _minValue);
            get => _maxValue;
        }

        public float AbsoluteValue => _value / _maxValue;

        public void ResetOnMax()
        {
            _value = _maxValue;
        }

        public void ResetOnMin()
        {
            _value = _minValue;
        }
    }
}