using System;
using Core.DI;
using Game;
using TMPro;
using UnityEngine;

namespace UI
{
    public class UIPausePage : UIBaseItem
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

        public void OnResume()
        {
            _gameProgress?.SetPausedState(false);
            _uiController.ShowPage<UIHudPage>();
        }

        public void OnRestart()
        {
            _gameProgress.RestartLevel();
            _uiController.ShowPage<UIHudPage>();
        }
    }
}