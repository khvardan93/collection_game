using System.Collections.Generic;

namespace Core.Services
{
    public interface IGameConfigs
    {
        IReadOnlyList<LevelConfig> Levels { get; }
    }
}