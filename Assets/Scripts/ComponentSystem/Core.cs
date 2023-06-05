using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Component = ComponentSystem.Component;

namespace Components
{
    public class Core : MonoBehaviour
    {
        private List<Component> _components;

        public void Awake()
        {
            _components = new List<Component>();
            
            Component[] components = GetComponentsInChildren<Component>();
            
            _components.AddRange(components);
        }

        public T GetCoreComponent<T>() where T : Component
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