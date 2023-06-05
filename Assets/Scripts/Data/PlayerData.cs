using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName ="PlayerData", menuName ="Data/Player Data")]
    public class PlayerData : ScriptableObject
    {
        [Header("Move State")]
        [SerializeField] private float _movementSpeed;
        
        [Header("Stats")]
        [SerializeField] private int _maxHealthPoint;

        public int MaxHealthPoints => _maxHealthPoint;
        public float MovementSpeed => _movementSpeed;
    }
}