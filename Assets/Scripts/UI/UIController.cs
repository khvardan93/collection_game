using Core.DI;
using Game;
using UnityEngine;

namespace UI
{
    public class UIController : MonoBehaviour, IUIController
    {
        [SerializeField] private UIBaseItem[] _pages;

        private IGameProgress _gameProgress;

        private void Awake()
        {
            _gameProgress = ServiceLocator.Container.Resolve<IGameProgress>();
            _gameProgress.OnLevelCompleted += OnLevelCompleted;
            _gameProgress.OnLevelFailed += OnLevelFailed;
        }

        private void OnDestroy()
        {
            _gameProgress.OnLevelCompleted -= OnLevelCompleted;
            _gameProgress.OnLevelFailed -= OnLevelFailed;
            _gameProgress = null;
        }

        private void OnLevelCompleted()
        {
            ShowPage<UIWinPage>();
        }

        private void OnLevelFailed()
        {
            ShowPage<UILosePage>();
        }

        public void ShowPage<PageType>() where PageType : UIBaseItem
        {
            foreach (var page in _pages)
            {
                if (page is PageType)
                {
                    page.Show();
                }
                else
                {
                    page.Hide();
                }
            }
        }
    }
}