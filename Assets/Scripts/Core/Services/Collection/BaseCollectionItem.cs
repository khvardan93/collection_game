using UnityEngine;

namespace Core.Services
{
    public abstract class BaseCollectionItem : ScriptableObject, ICollectionItem
    {
        [SerializeField] protected CollectionItemType _type;
        [SerializeField] protected string _name;
        
        CollectionItemType ICollectionItem.Type => _type;
        string ICollectionItem.Name => _name;
    }
}