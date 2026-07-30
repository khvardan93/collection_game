using Core.DI;
using Game;

namespace UI
{
    public class UIHudPage : UIBaseItem
    {
        private IUIController _uiController;
        private IGameProgress _gameProgress;
        
        private void Awake()
        {
            _uiController = ServiceLocator.Container.Resolve<IUIController>();
            _gameProgress = ServiceLocator.Container.Resolve<IGameProgress>();
        }

        private void OnDestroy()
        {
            _uiController = null;
        }

        public void OnPause()
        {
            _gameProgress?.SetPausedState(true);
            _uiController.ShowPage<UIPausePage>();
        }
    }
}