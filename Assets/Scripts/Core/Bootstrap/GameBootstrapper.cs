using Core.DI;
using Core.Services;
using UnityEngine;

namespace Core.Bootstrap
{
    [DefaultExecutionOrder(-1000)]
    public sealed class GameBootstrapper : MonoBehaviour
    {
        private void Awake()
        {
            var container = new ServiceContainer();

            container.Register<IGameClock, GameClock>();
            container.Register<IDataStorage, DataStorage>();

            ServiceLocator.Container = container;
        }

        private void OnDestroy()
        {
            ServiceLocator.Reset();
        }
    }
}
