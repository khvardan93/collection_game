using UnityEngine;

namespace Core.Services
{
    public abstract class BaseCollectionItem : ScriptableObject, ICollectionItem
    {
        [SerializeField] protected CollectionItemType _type;
        [SerializeField] protected string _name;
        
        public CollectionItemType Type => _type;
        public string Name => _name;
    }
}