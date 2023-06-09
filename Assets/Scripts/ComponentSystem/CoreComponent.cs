using System;
using Components;
using UnityEngine;

namespace ComponentSystem
{
    public abstract class CoreComponent : MonoBehaviour
    {
        protected Core core;

        protected virtual void Awake()
        {
            core = GetComponentInParent<Core>();
        }

        protected virtual void Start()
        {
            core.AddCoreComponent(this);
        }

        protected virtual void LogicUpdate() {}

        private void Update()
        {
            LogicUpdate();
        }

        public virtual void Turn() {}
    }
}