using Core.Services;

namespace Game
{
    public interface ICollectionProgress
    {
        CollectionItemType Type { get;}
        string Name { get;}
        int Count { get;}
        void AddProgress(int progress);
    }
}