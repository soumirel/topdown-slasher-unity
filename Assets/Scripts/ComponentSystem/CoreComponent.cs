using System;
using UnityEngine;

namespace ComponentSystem
{
    public class CoreComponent : MonoBehaviour
    {
        protected virtual void LogicUpdate() { }

        public void Update()
        {
            LogicUpdate();
        }
    }
}