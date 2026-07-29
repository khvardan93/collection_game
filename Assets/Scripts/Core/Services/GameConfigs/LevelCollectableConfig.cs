using UnityEngine;

namespace Core.Services
{
    [System.Serializable]
    public class LevelCollectableConfig : ILevelCollectableConfig
    {
        [SerializeField] private CollectionItemType _type;
        [SerializeField] private string _name;
        [SerializeField] private int _count;

        CollectionItemType ILevelCollectableConfig.Type => _type;

        string ILevelCollectableConfig.Name => _name;

        int ILevelCollectableConfig.count => _count;
    }
}