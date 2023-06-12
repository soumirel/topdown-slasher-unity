using UnityEngine;


namespace Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        public WeaponClass Class { get; private set; }
    }
}