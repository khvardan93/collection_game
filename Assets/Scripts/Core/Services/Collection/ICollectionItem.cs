namespace Core.Services
{
    public interface ICollectionItem
    {
        CollectionItemType Type { get; }
        string Name { get; }
    }
}