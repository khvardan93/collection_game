using Core.DI;
using Game;

namespace UI
{
    public class UIPausePage : UIBaseItem
    {
        private IGameProgress _gameProgress;
        private IUIController _uiController;

        private void Awake()
        {
            _gameProgress = ServiceLocator.Container.Resolve<IGameProgress>();
            _uiController = ServiceLocator.Container.Resolve<IUIController>();
        }

        public void OnResume()
        {
            _gameProgress?.SetPausedState(false);
            _uiController.ShowPage<UIHudPage>();
        }

        public void OnRestart()
        {
            
        }
    }
}