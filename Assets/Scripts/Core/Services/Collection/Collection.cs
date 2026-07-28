using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core.Services
{
    [CreateAssetMenu(fileName = "Collection", menuName = "Configs/Collection")]
    public class Collection : ScriptableObject, ICollection
    {
        [SerializeField] private BaseCollectionItem[] _items;

        IReadOnlyList<ICollectionItem> ICollection.Items => _items;

        bool ICollection.TryGet(CollectionItemType type, string name, out ICollectionItem item)
        {
            item = _items.FirstOrDefault(i => i.Type == type && i.Name == name);
            return item != null;
        }

        bool ICollection.TryGet(CollectionItemType type, out IReadOnlyList<ICollectionItem> items)
        {
            items = _items.Where(i => i.Type == type).ToList();
            return items.Count > 0;
        }
    }
}