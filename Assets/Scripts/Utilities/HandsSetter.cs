using ComponentSystem.Components;
using UnityEngine;
using UnityEngine.Serialization;

namespace Utilities
{
    [ExecuteInEditMode]
    public class HandsSetter : MonoBehaviour
    {
        public Vector2 Coordinates;
        
        public float MaxDirectionX;
        public float MinDirectionX;
        
        public float MaxDirectionY;
        public float MinDirectionY;
        
        public float a;
        public float b;
        
        [SerializeField] public Ellipse Ellipse;
        [SerializeField] public GameObject Hands;
        [SerializeField] public HandsCoreComponent HandsCoreComponent;
        
        public void OnValidate()
        {
            UpdatePositions();
        }

        public void UpdatePositions()
        {
            Ellipse.radius = new Vector2(a, b);
            Ellipse.Position = Coordinates;

            HandsCoreComponent._ellipseTrajectory_a = a;
            HandsCoreComponent._ellipseTrajectory_b = b;
            HandsCoreComponent._maxDirectionX = MaxDirectionX;
            HandsCoreComponent._minDirectionX = MinDirectionX;
            HandsCoreComponent._minDirectionY = MinDirectionY;
            HandsCoreComponent._maxDirectionY = MaxDirectionY;
            
            Hands.transform.position = Coordinates;
            
            Ellipse.UpdateEllipse();
        }
    }
}