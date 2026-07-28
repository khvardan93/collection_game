using System;
using UnityEngine;

namespace Core.Services
{
    [Serializable]
    public class InventoryItem : IInventoryItem, IStorable
    {
        [SerializeField] private CollectionItemType _type;
        [SerializeField]  private string _name;
        [SerializeField]  private int _count;
        
        CollectionItemType IInventoryItem.Type => _type;
        string IInventoryItem.Name => _name;
        int IInventoryItem.Count => _count;
        
        string IStorable.Key => $"InventoryItem_{_type}_{_name}";
    }
}