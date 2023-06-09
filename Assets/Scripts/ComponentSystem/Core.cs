using System;
using System.Collections.Generic;
using System.Linq;
using ComponentSystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace Components
{
    public class Core : MonoBehaviour
    {
        private List<CoreComponent> _components;
        
        public int FacingDirection { get; private set; }

        public void Awake()
        {
            _components = new List<CoreComponent>();
            
            // CoreComponent[] components = GetComponentsInChildren<CoreComponent>();
            //
            // _components.AddRange(components);
            
            FacingDirection = 1;
        }

        public void AddCoreComponent(CoreComponent component)
        {
            _components.Add(component);
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
        
        public T GetCoreComponent<T>(ref T value) where T : CoreComponent
        {
            value = GetCoreComponent<T>();
            return value;
        }
        
        public bool CheckTurnNeed(Vector2 direction)
        {
            return (int)Mathf.Sign(direction.x) != FacingDirection;
        }

        public virtual void Turn()
        {
            FacingDirection *= -1;
            foreach (var component in _components)
            {
                component.Turn();
            }
        }
    }
}