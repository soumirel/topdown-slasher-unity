using System.Collections.Generic;
using Animations;
using UnityEngine;

namespace Interfaces
{
    public interface IAnimated
    {
        Animator Animator { get; }
        SpriteRenderer SpriteRenderer { get; }
        
        List<string> AnimationTransitionTags { get; }
    }
}