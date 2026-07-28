using System.Collections.Generic;

namespace Core.Services
{
    public class Inventory : IInventory
    {
        private List<InventoryItem> _items;
        
        public IReadOnlyList<IInventoryItem> Items => _items;
    }
}