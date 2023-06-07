using System;
using UnityEngine;
using UnityEngine.Serialization;
using Utilities;

namespace Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        public event Action OnReady;

        [SerializeField] protected float cooldownSeconds;
        protected bool isReady;
        protected Timer cooldownTimer;


        public bool IsReady => isReady;


        public abstract void Use();

        protected virtual void Awake()
        {
            isReady = true;
            cooldownTimer = new Timer(cooldownSeconds, AllowUse);
        }

        protected virtual void Update()
        {
            cooldownTimer.Update();
        }

        protected virtual void AllowUse()
        {
            isReady = true;
            OnReady?.Invoke();
        }

        public virtual void Rotate(Vector2 lookAtPosition)
        {
            float rotateZ = Mathf.Atan2(lookAtPosition.y - transform.localPosition.y,
                lookAtPosition.x - transform.localPosition.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, 0, rotateZ - 90f);
        }
    }
}