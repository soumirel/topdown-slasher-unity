using System;
using UnityEngine;

namespace Weapons
{
    public class Excalibur : Weapon
    {
        private Collider2D _collider;

        protected override void Awake()
        {
            base.Awake();
            _collider = GetComponent<Collider2D>();
        }

        private void OnEnable()
        {
            _collider.enabled = false;
        }

        protected override void Attack()
        {
            _collider.enabled = true;
            Animator.SetTrigger("swing");
        }


        protected override void FinishAttack()
        {
            base.FinishAttack();
            _collider.enabled = false;
        }
    }
}