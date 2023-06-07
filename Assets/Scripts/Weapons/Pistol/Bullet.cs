using Interfaces;
using UnityEngine;

namespace Weapons.Pistol
{
    public class Bullet : MonoBehaviour, IPoolObject
    {
        private Rigidbody2D _rb;
        public Rigidbody2D Rb => _rb;

        public void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _rb.Sleep();
        }

        public void Prepare()
        {
            _rb.position = Vector2.zero;
            _rb.rotation = 0f;
            _rb.velocity = Vector2.zero;
            _rb.angularVelocity = 0f;
            _rb.inertia = 0f;
            _rb.WakeUp();
        }

        public void Clear()
        {
            _rb.Sleep();
        }

        public void OnCollisionEnter2D(Collision2D other)
        {
            Clear();
            gameObject.SetActive(false);
        }
    }
}