using System.Collections.Generic;
using Core.DI;
using Core.Services;
using Game;
using UnityEngine;

namespace UI
{
    public class UILevelCollectedItems : MonoBehaviour
    {
        [SerializeField] private UILevelCollectedItem[] _items;
        
        private IGameProgress  _gameProgress;
        private ICollectionStorage _collectionStorage;

        private void Awake()
        {
            _gameProgress = ServiceLocator.Container.Resolve<IGameProgress>();
            _collectionStorage = ServiceLocator.Container.Resolve<ICollectionStorage>();
        }

        private void OnEnable()
        {
            Refresh();
            
            _gameProgress.OnItemCollected += Refresh;
        }

        private void OnDisable()
        {
            _gameProgress.OnItemCollected -= Refresh;
        }

        private void Refresh()
        {
            IReadOnlyList<ICollectionProgress> collectionItems = _gameProgress.CurrentLevelProgress.CollectionItems;
            UILevelCollectedItem[] items = _items;
            
            if(collectionItems.Count > _items.Length)
                Debug.LogWarning("There are less UI items than the level progress.");
            
            for (int i = 0; i < items.Length; i++)
            {
                if (i >= collectionItems.Count)
                {
                    items[i].Hide();
                    continue;
                }

                ICollectionProgress collectionItem = collectionItems[i]; 
                
                if(_collectionStorage.TryGet(collectionItem.Type, collectionItem.Name, out ICollectionItem collectedItem))
                {
                    items[i].Setup(collectedItem.Sprite, collectionItem.Count, collectionItem.Target);
                }
            }
        }
    }
}