using Core.DI;
using Core.Services;
using Game;
using UI;
using UnityEngine;

namespace Core.Bootstrap
{
    [DefaultExecutionOrder(-1000)]
    public sealed class GameBootstrapper : MonoBehaviour
    {
        [SerializeField] private CollectionStorage _collectionStorage;
        [SerializeField] private UIController _uiController;
        [SerializeField] private GameConfigs _gameConfigs;
        
        private IGameTimer _gameTimer;
        
        private void Awake()
        {
            var container = new ServiceContainer();

            container.Register<IGameTimer, GameTimer>();
            container.Register<IDataStorage, DataStorage>();
            container.Register<ICollectionStorage>(_collectionStorage);
            container.Register<IGameConfigs>(_gameConfigs);
            container.Register<IInventory, Inventory>();
            container.Register<IGameProgress, GameProgress>();
            container.Register<IUIController>(_uiController);

            ServiceLocator.Container = container;
            
            _gameTimer = ServiceLocator.Container.Resolve<IGameTimer>();
        }

        private void Update()
        {
            _gameTimer.Tick(Time.deltaTime);
        }

        private void OnDestroy()
        {
            ServiceLocator.Destroy();
        }
    }
}
