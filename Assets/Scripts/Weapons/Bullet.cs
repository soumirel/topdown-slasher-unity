using System;
using Interfaces;
using UnityEngine;

namespace Weapons
{
    public class Bullet : MonoBehaviour, IPoolObject
    {
        private Rigidbody2D _rb;
        public Rigidbody2D Rb => _rb;

        public void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        public void Prepare()
        {
            Rb.position = Vector2.zero;
            Rb.rotation = 0f;
            Rb.velocity = Vector2.zero;
            Rb.angularVelocity = 0f;
        }

        public void Clear()
        {
            
        }

        public void OnCollisionEnter2D(Collision2D other)
        {
            
            
            gameObject.SetActive(false);
        }
    }
}