using System.Collections.Generic;
using Animations;
using UnityEngine;

namespace Interfaces
{
    public interface IAnimated
    {
        Animator Animator { get; }

        AnyStateAnimator AnyStateAnimator { get; }
    }
}