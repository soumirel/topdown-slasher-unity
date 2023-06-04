using System;
using UnityEngine;
using Utilities;

namespace Weapons
{
    public class Weapon : MonoBehaviour
    {
        public event Action OnReady;

        [SerializeField] private Transform _shootPosition;
        
        [SerializeField] private BulletsPool _bulletsPool;

        [SerializeField] private float _bulletForce;
        
        [SerializeField] private float _cooldownSeconds;

        private bool _isReady;

        private Timer _cooldownTimer;

        public bool IsReady => _isReady;

        private void Awake()
        {
            _isReady = true;
            _cooldownTimer = new Timer(_cooldownSeconds, AllowUse);
        }

        private void Update()
        {
            _cooldownTimer.Update();
        }

        private void AllowUse()
        {
            _isReady = true;
            OnReady?.Invoke();
        }

        public void Rotate(float angle)
        {
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        public void Use()
        {
            _cooldownTimer.StartTimer();
            var bullet = _bulletsPool.Get();
            bullet.transform.position = _shootPosition.position;
            bullet.transform.rotation = _shootPosition.rotation;
            bullet.Rb.AddForce(_shootPosition.up * _bulletForce);
            _isReady = false;
        }
    }
}