using System.Collections.Generic;
using UnityEngine;

namespace Core.Services
{
    [CreateAssetMenu(fileName = "GameConfigs", menuName = "Configs/GameConfigs")]
    public class GameConfigs : ScriptableObject, IGameConfigs
    {
        [SerializeField] private LevelConfig[]  _levels;
        IReadOnlyList<LevelConfig> IGameConfigs.Levels => _levels;
    }
}