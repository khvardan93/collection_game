using Core.DI;
using Game;

namespace UI
{
    public class UIEntrancePage : UIBaseItem
    {
        private IGameProgress _gameProgress;
        private IUIController _uiController;

        private void Awake()
        {
            _gameProgress = ServiceLocator.Container.Resolve<IGameProgress>();
            _uiController = ServiceLocator.Container.Resolve<IUIController>();
        }

        public void OnPlay()
        {
            if (_gameProgress.TrySetLevel())
            {
                _uiController.ShowPage<UIHudPage>();
            }
        }

        public void OnSettings()
        {
            
        }
    }
}