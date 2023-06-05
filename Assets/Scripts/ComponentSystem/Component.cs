using System;
using UnityEngine;

namespace ComponentSystem
{
    public class Component : MonoBehaviour
    {
        protected virtual void LogicUpdate() { }

        public void Update()
        {
            LogicUpdate();
        }
    }
}