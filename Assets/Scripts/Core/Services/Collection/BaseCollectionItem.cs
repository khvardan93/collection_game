using GamePlay;
using UnityEngine;

namespace Core.Services
{
    public abstract class BaseCollectionItem : ScriptableObject, ICollectionItem
    {
        [SerializeField] protected CollectionItemType _type;
        [SerializeField] protected string _name;
        [SerializeField] protected Sprite _sprite;
        [SerializeField] protected CollectableController _prefab;

        public CollectionItemType Type => _type;
        public string Name => _name;
        Sprite ICollectionItem.Sprite => _sprite;
        CollectableController ICollectionItem.Prefab => _prefab;
    }
}