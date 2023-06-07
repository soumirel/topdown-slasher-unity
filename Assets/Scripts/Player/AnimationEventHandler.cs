using System;
using UnityEngine;

namespace Player
{
    public class AnimationEventHandler : MonoBehaviour
    {
        public event Action OnAnimationStart;
        public event Action OnAnimationFinish;
        public event Action OnShotFrame;

        public void AnimationStartTrigger()
        {
            OnAnimationStart?.Invoke();
        }

        public void AnimationFinishedTrigger()
        {
            OnAnimationFinish?.Invoke();
        }

        public void ShotFrameTrigger()
        {
            OnShotFrame?.Invoke();
        }
    }
}