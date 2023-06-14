using System;
using Animations;
using Interfaces;
using UnityEngine;


namespace Weapons
{
    public abstract class Weapon : MonoBehaviour, IAnimated
    {
        public event Action OnUseAvailable;
        
        public Animator Animator { get; private set; }
        public AnyStateAnimator AnyStateAnimator { get; private set;  }

        public virtual void Use()
        {
            Attack();
        }

        protected abstract void Attack();

        protected virtual void FinishAttack()
        {
            OnUseAvailable?.Invoke();
        }

        protected virtual void Awake()
        {
            Animator = GetComponent<Animator>();
            AnyStateAnimator = GetComponent<AnyStateAnimator>();
        }
    }
}