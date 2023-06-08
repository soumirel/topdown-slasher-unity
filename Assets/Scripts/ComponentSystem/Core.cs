using System;
using System.Collections.Generic;
using System.Linq;
using ComponentSystem;
using UnityEngine;

namespace Components
{
    public class Core : MonoBehaviour
    {
        private List<CoreComponent> _components;

        public void Awake()
        {
            _components = new List<CoreComponent>();
            
            CoreComponent[] components = GetComponentsInChildren<CoreComponent>();
            
            _components.AddRange(components);
        }

        public T GetCoreComponent<T>() where T : CoreComponent
        {
            var component = _components.OfType<T>().FirstOrDefault();

            if (component)
            {
                return component;
            }
            
            Debug.LogWarning($"{typeof(T)} not found on {transform.parent.name}");
            return null;
        }
    }
}