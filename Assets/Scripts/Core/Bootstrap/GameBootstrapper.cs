using Core.DI;
using Core.Services;
using UI;
using UnityEngine;

namespace Core.Bootstrap
{
    [DefaultExecutionOrder(-1000)]
    public sealed class GameBootstrapper : MonoBehaviour
    {
        [SerializeField] private Collection _collection;
        [SerializeField] private UIController _uiController;
        
        private void Awake()
        {
            var container = new ServiceContainer();

            container.Register<IGameClock, GameClock>();
            container.Register<IDataStorage, DataStorage>();
            container.Register<ICollection>(_collection);
            container.Register<IInventory, Inventory>();
            container.Register<IUIController>(_uiController);

            ServiceLocator.Container = container;
        }

        private void OnDestroy()
        {
            ServiceLocator.Reset();
        }
    }
}
