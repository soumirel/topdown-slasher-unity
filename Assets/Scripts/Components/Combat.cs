using System;
using UnityEngine;
using Weapons;

namespace Components
{
    public class Combat : MonoBehaviour
    {
        //public event Action OnAttackFinish;

        private bool _isAttack;
        
        [SerializeField] private Hand _hand;

        private void OnEnable()
        {
            _hand.OnWeaponUsingFinish += FinishAttack;
        }

        public void Attack()
        {
            if (!_isAttack)
            {
                _hand.UseWeapon();
            }
        }

        public void FinishAttack()
        {
            _isAttack = false;
        }
    }
}