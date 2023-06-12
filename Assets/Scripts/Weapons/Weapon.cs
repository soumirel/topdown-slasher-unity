using System;
using UnityEngine;


namespace Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        //public WeaponClass Class { get; private set; }

        private Collider2D _collider;
        private Animator _animator;


        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _collider = GetComponent<Collider2D>();
        }


        private void OnEnable()
        {
            _collider.enabled = false;
        }

        public void Activate()
        {
            Debug.Log(nameof(Activate));
            _collider.enabled = true;
            _animator.SetTrigger("swing");
        }


        private void Deactivate()
        {
            _collider.enabled = false;
        }
    }
}