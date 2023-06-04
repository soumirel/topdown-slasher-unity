using System;
using UnityEngine;

namespace Utilities
{
    public class Timer
    {
        public float CountdownTime { get; private set; }
        public bool IsRunning { get; private set; }

        private float currentTime;
        private Action onTimerEnd;

        public Timer(float countdownTime, Action onTimerEnd)
        {
            CountdownTime = countdownTime;
            this.onTimerEnd = onTimerEnd;
        }

        public void Update()
        {
            if (IsRunning)
            {
                currentTime -= Time.deltaTime;

                if (currentTime <= 0f)
                {
                    currentTime = 0f;
                    TimerEnd();
                }
            }
        }

        public void StartTimer()
        {
            currentTime = CountdownTime;
            IsRunning = true;
        }

        public void StopTimer()
        {
            IsRunning = false;
        }

        private void TimerEnd()
        {
            IsRunning = false;
            onTimerEnd?.Invoke();
        }
    }
}