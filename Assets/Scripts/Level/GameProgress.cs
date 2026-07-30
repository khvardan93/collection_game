using System;
using Core.DI;
using Core.Services;

namespace Game
{
    public class GameProgress : IGameProgress, IDestroyable
    {
        public event Action OnLevelCompleted;
        public event Action OnLevelFailed;
        public event Action OnItemCollected;
        
        private readonly IDataStorage _dataStorage;
        private readonly IGameConfigs _gameConfigs;
        private readonly IInventory _inventory;
        private readonly IGameTimer _gameTimer;
        
        private GameProgressData _gameProgressData;
        private ILevelProgress _levelProgress;
        private bool _isLevelActive;
        
        int IGameProgress.CurrentLevelIndex => _gameProgressData.CurrentLevelIndex;
        ILevelProgress IGameProgress.CurrentLevelProgress => _levelProgress;
        bool IGameProgress.IsLevelActive => _isLevelActive;

        public GameProgress()
        {
            _dataStorage = ServiceLocator.Container.Resolve<IDataStorage>();
            _gameConfigs = ServiceLocator.Container.Resolve<IGameConfigs>();
            _inventory = ServiceLocator.Container.Resolve<IInventory>();
            _gameTimer = ServiceLocator.Container.Resolve<IGameTimer>();
            _gameProgressData = _dataStorage.Load<GameProgressData>(GameProgressData.DataKey);

            _inventory.OnPlaced += OnItemPlaced;
            _gameTimer.OnTimerFinished += OnTimerFinished;
        }
        
        void IDestroyable.Destroy()
        {
            _inventory.OnPlaced -= OnItemPlaced;
            _gameTimer.OnTimerFinished -= OnTimerFinished;
        }

        private void OnTimerFinished()
        {
            if(!_isLevelActive) return;
            
            _isLevelActive = false;
            
            OnLevelFailed?.Invoke();
        }

        private void OnItemPlaced(CollectionItemType type, string name, int count)
        {
            foreach (var item in _levelProgress.CollectionItems)
            {
                if (item.Type == type && item.Name == name)
                {
                    item.AddProgress(count);
                    OnItemCollected?.Invoke();
                    CheckGameProgress();
                    return;
                }
            }
        }

        private void CheckGameProgress()
        {
            foreach (var item in _levelProgress.CollectionItems)
            {
                if(item.InProgress)
                    return;
            }

            _isLevelActive = false;
            _gameTimer.EndTimer();
            OnLevelCompleted?.Invoke();
        }

        public bool TrySetNextLevel()
        {
            _gameProgressData.CurrentLevelIndex++;
            _dataStorage.Save(_gameProgressData);
            return TrySetLevel();
        }

        public void RestartLevel()
        {
            TrySetLevel();
        }

        public void SetPausedState(bool paused)
        {
            _gameTimer.PauseTimer(paused);
            _isLevelActive = !paused;
        }

        public bool TrySetLevel()
        {
            if(_gameProgressData.CurrentLevelIndex <_gameConfigs.Levels.Count)
            {
                var currentLevel = _gameConfigs.Levels[_gameProgressData.CurrentLevelIndex];
                _levelProgress = new LevelProgress(currentLevel);
                _gameTimer.StartTimer(currentLevel.Duration);
                _isLevelActive = true;
                OnItemCollected?.Invoke();
                return true;
            }
            
            return false;
        }
        
        [Serializable]
        public struct GameProgressData : IStorable
        {
            public const string DataKey = "GameProgress";
            public int CurrentLevelIndex;
            public string Key => DataKey;
        }
    }
}