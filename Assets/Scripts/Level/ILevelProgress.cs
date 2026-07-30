using System.Collections.Generic;

namespace Game
{
    public interface ILevelProgress
    {
        IReadOnlyList<ICollectionProgress>  CollectionItems { get; }
    }
}