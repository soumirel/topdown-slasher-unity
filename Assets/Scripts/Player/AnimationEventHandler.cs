using System;
using UnityEngine;

namespace Player
{
    public class AnimationEventHandler : MonoBehaviour
    {
        public event Action OnAnimationFinish;

        public event Action OnShotFrame;

        public void AnimationFinishedTrigger() => OnAnimationFinish?.Invoke();

        public void ShotFrameTrigger() => OnShotFrame?.Invoke();
    }
}