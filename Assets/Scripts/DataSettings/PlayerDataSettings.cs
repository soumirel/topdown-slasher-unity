using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName ="PlayerData", menuName ="Data/Player Data")]
    public class PlayerDataSettings : ScriptableObject
    {
        
        [SerializeField] private float _movementSpeed;
        [SerializeField] private float _turnSpeed;
        
        
        [SerializeField] private int _maxHealthPoint;

        
        public int MaxHealthPoints => _maxHealthPoint;
        public float MovementSpeed => _movementSpeed;
        public float TurnSpeed => _turnSpeed;
    }
}