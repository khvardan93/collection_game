using System;

namespace Core.Services
{
    public interface IInventoryItem
    {
        CollectionItemType Type { get; }
        String Name { get; }
        int Count { get; }
    }
}