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
        void SetNextLevel();
        void SetPausedState(bool paused);
    }
}