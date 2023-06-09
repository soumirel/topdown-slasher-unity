using System;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

namespace ComponentSystem.Components
{
    public class CombatCoreComponent : CoreComponent
    {
        [SerializeField] private List<Weapon> _weapons;

        public Weapon CurrentWeapon { get; private set; }
        public bool IsReady { get; private set; }
        public bool IsAimed { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            _weapons ??= new List<Weapon>();
            if (_weapons.Count > 0)
            {
                CurrentWeapon = _weapons[0];
                IsReady = true;
            }
            else
            {
                IsReady = false;
            }
        }


        public void Attack()
        {
            if (IsReady)
            {
                CurrentWeapon.Use();
            }
        }
    }
}