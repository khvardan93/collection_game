using Core.DI;
using Core.Services;
using Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIAddTimePage : UIBaseItem
    {
        [SerializeField] private TMP_Text _timeCountText;
        [SerializeField] private Button _addTimeButton;
        [SerializeField] private TMP_Text _hintText;
        
        private IInventory _inventory;
        private IGameTimer _gameTimer;
        private IUIController _uiController;
        private ICollectionStorage _collectionStorage;
        private IGameProgress _gameProgress;

        private IInventoryItem _timeInventoryItem;
        private TimeCollectionItem _timeCollectionItem;

        private void Awake()
        {
            _inventory = ServiceLocator.Container.Resolve<IInventory>();
            _gameTimer = ServiceLocator.Container.Resolve<IGameTimer>();
            _uiController = ServiceLocator.Container.Resolve<IUIController>();
            _gameProgress = ServiceLocator.Container.Resolve<IGameProgress>();
            _collectionStorage = ServiceLocator.Container.Resolve<ICollectionStorage>();
        }

        private void OnEnable()
        {
            if (_collectionStorage.TryGet(CollectionItemType.Time, "Time", out var collectionItem) &&
                collectionItem is TimeCollectionItem timeCollectionItem)
            {
                _timeCollectionItem = timeCollectionItem;
            }
            else
            {
                Debug.LogError($"Collection {CollectionItemType.Time} not found");
            }
            ShowCountText();
        }

        private void ShowCountText()
        {
            if(_inventory.TryGet(CollectionItemType.Time, "Time", out _timeInventoryItem))
            {
                var count = _timeInventoryItem.Count;
                _timeCountText.text = count.ToString();
                _hintText.gameObject.SetActive(count == 0);
                _addTimeButton.gameObject.SetActive(count > 0);
            }
        }
        
        public void OnAddTime()
        {
            if (_timeInventoryItem == null || _timeInventoryItem.Count == 0) return;
            
            _inventory.Extract(CollectionItemType.Time, "Time", 1);
            _gameTimer.AddTime(_timeCollectionItem.TimeToAdd);
            OnClose();
        }

        public void OnClose()
        {
            _gameProgress?.SetPausedState(false);
            _uiController.ShowPage<UIHudPage>();
        }
    }
}