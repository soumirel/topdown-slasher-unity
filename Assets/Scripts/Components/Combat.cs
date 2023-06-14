using System;
using UnityEngine;
using UnityEngine.Serialization;
using Weapons;

namespace Components
{
    public class Combat : MonoBehaviour
    {
        [SerializeField] private Weapon _excalibur;

        [SerializeField] private Hand _primaryWeapon;
        [SerializeField] private Hand _secondaryWeapon;


        public void Start()
        {
            _primaryWeapon.HandleWeapon(_excalibur);
        }

        public void Attack()
        {
            _primaryWeapon.UseWeapon();
        }
    }
}