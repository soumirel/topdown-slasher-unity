using System;
using UnityEngine;

namespace Player
{
    public class AnimationEventHandler : MonoBehaviour
    {
        public event Action OnTurnFinish;

        public void TurnAnimationFinish()
        {
            OnTurnFinish?.Invoke();
        }
    }
}