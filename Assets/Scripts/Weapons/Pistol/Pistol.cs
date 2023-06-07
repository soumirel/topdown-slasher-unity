using UnityEngine;

namespace Weapons.Pistol
{
    public class Pistol : Weapon
    {
        [SerializeField] private BulletsPool _bulletsPool;
        
        [SerializeField] private Transform _shootPosition;
        [SerializeField] private float _bulletForce;
        public override void Use()
        {
            cooldownTimer.StartTimer();
            var bullet = _bulletsPool.Get();
            
            bullet.transform.position = _shootPosition.position;
            bullet.transform.rotation = _shootPosition.rotation;
            
            bullet.Rb.AddForce(_shootPosition.up * _bulletForce);
            isReady = false;
        }
    }
}