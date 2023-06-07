using System.Collections.Generic;
using UnityEngine;

namespace Weapons.Pistol
{
    public class BulletsPool : MonoBehaviour
    {
        [SerializeField] private Bullet _bulletPrefab;

        [Range(2, 50)] [SerializeField] private int _size;

        private Queue<Bullet> _bullets;

        public void Awake()
        {
            _bullets = new Queue<Bullet>(_size);
 
            for (int i = 0; i < _size; i++)
            {
                var bullet = Instantiate(_bulletPrefab, transform);
                bullet.gameObject.SetActive(false);
                _bullets.Enqueue(bullet);
            }
        }

        public Bullet Get()
        {
            var bullet = _bullets.Dequeue();
            _bullets.Enqueue(bullet);
            
            bullet.Prepare();
            bullet.gameObject.SetActive(true);
            
            return bullet;
        }
    }
}