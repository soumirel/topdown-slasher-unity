using System;
using UnityEngine;

namespace Player
{
    public class AnimationEventHandler : MonoBehaviour
    {
        public event Action OnAnimationFinish;

        public event Action OnShotFrame;

        public void AnimationFinishedTrigger()
        {
            print("AnimationFinishedTrigger");
            OnAnimationFinish?.Invoke();
        }

        public void ShotFrameTrigger()
        {
            print("ShotFrameTrigger");
            OnShotFrame?.Invoke();
        }
    }
}