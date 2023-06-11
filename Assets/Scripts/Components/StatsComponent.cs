using System.Collections.Generic;
using ComponentSystem;
using ComponentSystem.Stats;
using Data;
using UnityEngine;

namespace Components
{
    public class StatsComponent : MonoBehaviour
    {
        [SerializeField] private List<StatDataSettings> _statsSettings;
        
        private Dictionary<StatType, Stat> _stats;

        protected void Awake()
        {
            _stats = new Dictionary<StatType, Stat>();
            
            foreach (var statSettings in _statsSettings)
            {
                _stats.TryAdd(statSettings.Type,
                    new Stat(statSettings.MaxValue, statSettings.MinValue));
            }
        }

        public Stat GetStat(StatType type)
        {
            if (_stats.TryGetValue(type, out var stat))
            {
                return stat;
            }
            
            Debug.LogWarning($"{type} stat not found on {transform.parent.name}");
            return null;
        }
    }
}