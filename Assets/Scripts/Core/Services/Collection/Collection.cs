using System.Collections.Generic;
using UnityEngine;

namespace Core.Services
{
    [CreateAssetMenu(fileName = "Collection", menuName = "Configs/Collection")]
    public class Collection : ScriptableObject, ICollection
    {
        [SerializeField] private BaseCollectionItem[] _items;

        IReadOnlyList<ICollectionItem> ICollection.Items => _items;
    }
}