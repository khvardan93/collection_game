using System;

namespace Game
{
    public interface IGameProgress
    {
        event Action OnLevelCompleted;
        event Action OnLevelFailed;
        event Action OnItemCollected;
        
        int CurrentLevelIndex { get; }
        ILevelProgress CurrentLevelProgress { get; }
        bool IsLevelActive { get; }

        bool TrySetLevel();
        bool TrySetNextLevel();
        void RestartLevel();
        void SetPausedState(bool paused);
    }
}