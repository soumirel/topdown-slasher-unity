using Components;
using UnityEngine;

namespace Interfaces
{
    public interface IMovable
    {
        Rigidbody2D Rb { get; set; }
        
        Movement Movement { get; }
        
        float MovementSpeed { get; set; }
    }
}