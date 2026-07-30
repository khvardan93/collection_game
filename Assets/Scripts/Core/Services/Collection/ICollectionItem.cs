using GamePlay;
using UnityEngine;

namespace Core.Services
{
    public interface ICollectionItem
    {
        CollectionItemType Type { get; }
        string Name { get; }
        Sprite Sprite { get; }
        CollectableAnimator Prefab { get; }
    }
}