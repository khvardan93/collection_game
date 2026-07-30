using System;

namespace Core.Services
{
    public interface IGameTimer
    {
        float RemainingTime { get; }
        bool IsRunning { get; }

        event Action OnTimerFinished;
        event Action<int> OnSecondTick;

        void StartTimer(float duration);
        void EndTimer();
        void PauseTimer(bool pause);
        void AddTime(float additionalTime);
        void Tick(float deltaTime);
    }
}
