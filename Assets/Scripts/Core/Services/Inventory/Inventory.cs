using System;
using System.Collections.Generic;
using System.Linq;
using Core.DI;
using UnityEngine;

namespace Core.Services
{
    [Serializable]
    public class Inventory : IInventory, IStorable
    {
        private const string _key = "Inventory";

        [SerializeField] private List<InventoryItem> _items = new ();
        private readonly IDataStorage _dataStorage;

        string IStorable.Key => _key;
        public event Action<CollectionItemType, string, int> OnPlaced;
        public event Action OnChanged;
        
        IReadOnlyList<IInventoryItem> IInventory.Items => _items;

        public Inventory()
        {
            _dataStorage = ServiceLocator.Container.Resolve<IDataStorage>();
            _dataStorage.Fill(_key, this);
        }

        void IInventory.Place(CollectionItemType type, string name, int quantity)
        {
            if (quantity <= 0)
            {
                return;
            }

            InventoryItem item = FindItem(type, name);

            if (item is null)
            {
                _items.Add(new InventoryItem(type, name, quantity));
            }
            else
            {
                item.Add(quantity);
            }

            OnPlaced?.Invoke(type, name, quantity);
            OnChanged?.Invoke();
            
            _dataStorage.Save(this);
        }

        void IInventory.Extract(CollectionItemType type, string name, int quantity)
        {
            if (quantity <= 0)
            {
                return;
            }

            InventoryItem item = FindItem(type, name);

            if (item is null)
            {
                return;
            }

            item.Remove(quantity);

            if (item.Count <= 0)
            {
                _items.Remove(item);
            }
            
            OnChanged?.Invoke();
            _dataStorage.Save(this);
        }

        bool IInventory.TryGet(CollectionItemType type, string name, out IInventoryItem  item)
        {
            item = FindItem(type, name);
            
            return item is not null;
        }

        bool IInventory.TryGet(CollectionItemType type, out IReadOnlyList<IInventoryItem> items)
        {
            items = _items.Where(item => item.Type == type).ToList();
            return items.Count > 0;
        }

        private InventoryItem FindItem(CollectionItemType type, string name)
        {
            return _items.Find(item => item.Type == type && item.Name == name);
        }
    }
}
