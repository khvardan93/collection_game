using Core.DI;
using Core.Services;
using UnityEngine;

namespace UI
{
    public class UIInventory : UIBaseItem
    {
        [SerializeField] private UIInventoryItem[] _items;
        
        private IInventory  _inventory;

        private void Start()
        {
            _inventory = ServiceLocator.Container.Resolve<IInventory>();
            _inventory.OnChanged += Refresh;
            Refresh();
        }

        private void OnDestroy()
        {
            _inventory.OnChanged -= Refresh;
        }
        
        private void Refresh()
        {
            int index = 0;
            foreach (IInventoryItem inventoryItem in _inventory.Items)
            {
                if(inventoryItem.Count == 0) continue;
                
                _items[index].Setup(inventoryItem);
                index++;
            }

            while (index < _items.Length)
            {
                _items[index].Hide();
                index++;
            }
        }
    }
}