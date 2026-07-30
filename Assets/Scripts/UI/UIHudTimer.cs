using Core.DI;
using Core.Services;
using TMPro;
using UnityEngine;

namespace UI
{
    public class UIHudTimer : MonoBehaviour
    {
        [SerializeField] private TMP_Text _timerText;

        private IGameTimer _gameTimer;

        private void Awake()
        {
            _gameTimer = ServiceLocator.Container.Resolve<IGameTimer>();
        }

        private void OnEnable()
        {
            _timerText.text = string.Empty;
            _gameTimer.OnSecondTick += OnSecondTick;
        }

        private void OnDisable()
        {
            _gameTimer.OnSecondTick -= OnSecondTick;
        }
        
        private void OnSecondTick(int seconds)
        {
            _timerText.text = $"{seconds / 60:00}:{seconds % 60:00}";
        }
    }
}