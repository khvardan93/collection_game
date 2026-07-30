using Core.DI;
using Core.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIInventoryItem : UIBaseItem
    {
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _countText;

        private ICollectionStorage _collectionStorage;

        public override void Hide()
        {
            base.Hide();
            _image.sprite = null;
            _countText.text = string.Empty;
        }
        
        public void Setup(IInventoryItem inventoryItem)
        {
            _collectionStorage ??= ServiceLocator.Container.Resolve<ICollectionStorage>();
            _countText.text = inventoryItem.Count.ToString();

            if (_collectionStorage.TryGet(inventoryItem.Type, inventoryItem.Name, out var collectionItem))
            {
                _image.sprite = collectionItem.Sprite;
            }
            
            Show();
        }
    }
}