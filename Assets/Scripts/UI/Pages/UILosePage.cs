using Core.DI;
using Game;
using TMPro;
using UnityEngine;

namespace UI
{
    public class UILosePage : UIBaseItem
    {
        [SerializeField] private TMP_Text _levelText;
        
        private IGameProgress _gameProgress;
        private IUIController _uiController;

        private void Awake()
        {
            _gameProgress = ServiceLocator.Container.Resolve<IGameProgress>();
            _uiController = ServiceLocator.Container.Resolve<IUIController>();
        }
        
        private void OnEnable()
        {
            _levelText.text = $"LEVEL : {_gameProgress.CurrentLevelIndex + 1}";
        }

        public void OnTryAgain()
        {
            _gameProgress.RestartLevel();
            _uiController.ShowPage<UIHudPage>();
        }
    }
}