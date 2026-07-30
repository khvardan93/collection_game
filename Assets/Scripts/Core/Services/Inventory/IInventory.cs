using System.Collections.Generic;
using System;

namespace Core.Services
{
    public interface IInventory
    {
        public event Action<CollectionItemType, string, int> OnPlaced;
        public event Action OnChanged;
        
        IReadOnlyList<IInventoryItem> Items { get; }

        void Place(CollectionItemType type, string name, int quantity);
        void Extract(CollectionItemType type, string name, int quantity);
        bool TryGet(CollectionItemType type, string name, out IInventoryItem  item);
        bool TryGet(CollectionItemType type, out IReadOnlyList<IInventoryItem> items);
    }
}