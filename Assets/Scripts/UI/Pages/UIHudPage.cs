using Core.DI;

namespace UI
{
    public class UIHudPage : UIBaseItem
    {
        private IUIController _uiController;
        
        private void Awake()
        {
            _uiController = ServiceLocator.Container.Resolve<IUIController>();
        }

        private void OnDestroy()
        {
            _uiController = null;
        }

        public void OnPause()
        {
            _uiController.ShowPage<UIPausePage>();
        }
    }
}