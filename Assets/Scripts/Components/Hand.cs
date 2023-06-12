using System;
using UnityEngine;
using Weapons;

namespace Components
{
    public class Hand : MonoBehaviour
    {
        public event Action OnWeaponUsingFinish;
        
        [SerializeField] private Weapon _currentWeapon;

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void UseWeapon()
        {
            _animator.SetTrigger("melee_swing");
            _currentWeapon.Activate();
        }

        public void FinishUsingWeapon()
        {
            OnWeaponUsingFinish?.Invoke();
        }
    }
}