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

        public CollectionItemType Type => _type;
        public string Name => _name;
        public int Count => _count;

        string IStorable.Key => $"InventoryItem_{_type}_{_name}";

        public InventoryItem()
        {
        }

        public InventoryItem(CollectionItemType type, string name, int count)
        {
            _type = type;
            _name = name;
            _count = count;
        }

        public void Add(int quantity)
        {
            _count += quantity;
        }

        public void Remove(int quantity)
        {
            _count = Mathf.Max(0, _count - quantity);
        }
    }
}