using System.Collections.Generic;

namespace Core.Services
{
    public interface ICollection
    {
        IReadOnlyList<ICollectionItem> Items { get; }
        
        bool TryGet(CollectionItemType type, string name, out ICollectionItem  item);
        bool TryGet(CollectionItemType type, out IReadOnlyList<ICollectionItem> items);
    }
}