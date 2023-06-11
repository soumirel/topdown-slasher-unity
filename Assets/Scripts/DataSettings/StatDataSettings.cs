using ComponentSystem.Stats;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName ="StatData", menuName ="Data/Stat Data")]
    public class StatDataSettings : ScriptableObject
    {
        [SerializeField] private StatType _statType = StatType.None;

        [SerializeField] private float _minValue;
        [SerializeField] private float _maxValue;

        public StatType Type => _statType;
        public float MinValue => _minValue;
        public float MaxValue => _maxValue;
    }
}