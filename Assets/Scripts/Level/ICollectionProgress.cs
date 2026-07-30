using Core.Services;

namespace Game
{
    public interface ICollectionProgress
    {
        CollectionItemType Type { get;}
        string Name { get;}
        int Target { get; }
        int Count { get;}
        bool InProgress { get;}
        
        void AddProgress(int progress);
    }
}