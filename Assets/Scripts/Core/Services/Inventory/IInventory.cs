using System.Collections.Generic;

namespace Core.Services
{
    public interface IInventory
    {
        IReadOnlyList<IInventoryItem> Items { get; }
    }
}