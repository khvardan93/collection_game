using System.Collections.Generic;

namespace Core.Services
{
    public interface ICollection
    {
        IReadOnlyList<ICollectionItem> Items { get; }
    }
}