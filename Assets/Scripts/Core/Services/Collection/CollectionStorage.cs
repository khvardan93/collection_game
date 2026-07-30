using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core.Services
{
    [CreateAssetMenu(fileName = "Collection", menuName = "Configs/Collection")]
    public class CollectionStorage : ScriptableObject, ICollectionStorage
    {
        [SerializeField] private BaseCollectionItem[] _items;

        IReadOnlyList<ICollectionItem> ICollectionStorage.Items => _items;

        bool ICollectionStorage.TryGet(CollectionItemType type, string name, out ICollectionItem item)
        {
            item = _items.FirstOrDefault(i => i.Type == type && i.Name == name);
            return item != null;
        }

        bool ICollectionStorage.TryGet(CollectionItemType type, out IReadOnlyList<ICollectionItem> items)
        {
            items = _items.Where(i => i.Type == type).ToList();
            return items.Count > 0;
        }
    }
}