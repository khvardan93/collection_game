using System.Collections.Generic;
using UnityEngine;

namespace Core.Services
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "Configs/LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        [SerializeField] private int _duration;
        [SerializeField] private LevelCollectableConfig[] _collectables;
        
        public int Duration => _duration;
        public IReadOnlyList<ILevelCollectableConfig> Collectables => _collectables;
    }
}