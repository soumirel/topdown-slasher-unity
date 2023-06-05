using System.Collections.Generic;
using ComponentSystem.Stats;
using Data;
using UnityEngine;

namespace ComponentSystem.Components
{
    public class StatsComponent : Component
    {
        [SerializeField] private List<StatData> _statsSettings;
        
        private Dictionary<StatType, Stat> _stats;

        public void Awake()
        {
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