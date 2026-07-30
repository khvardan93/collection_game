using System;
using UnityEngine;

namespace Core.Services
{
    public class GameTimer : IGameTimer
    {
        public float RemainingTime { get; private set; }
        public bool IsRunning { get; private set; }

        public event Action OnTimerFinished;
        public event Action<int> OnSecondTick;

        private int _lastReportedSecond;

        public void StartTimer(float duration)
        {
            RemainingTime = duration;
            IsRunning = true;
            _lastReportedSecond = Mathf.CeilToInt(RemainingTime);
            OnSecondTick?.Invoke(_lastReportedSecond);
        }

        public void AddTime(float additionalTime)
        {
            if (!IsRunning)
            {
                return;
            }

            RemainingTime += additionalTime;

            var currentSecond = Mathf.CeilToInt(RemainingTime);
            if (currentSecond != _lastReportedSecond)
            {
                _lastReportedSecond = currentSecond;
                OnSecondTick?.Invoke(currentSecond);
            }
        }

        public void Tick(float deltaTime)
        {
            if (!IsRunning)
            {
                return;
            }

            RemainingTime -= deltaTime;

            if (RemainingTime <= 0f)
            {
                RemainingTime = 0f;
                IsRunning = false;
                _lastReportedSecond = 0;
                OnSecondTick?.Invoke(0);
                OnTimerFinished?.Invoke();
                return;
            }

            var currentSecond = Mathf.CeilToInt(RemainingTime);
            if (currentSecond != _lastReportedSecond)
            {
                _lastReportedSecond = currentSecond;
                OnSecondTick?.Invoke(currentSecond);
            }
        }
    }
}
