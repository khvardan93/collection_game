namespace Core.Services
{
    public interface ILevelCollectableConfig
    {
        CollectionItemType Type { get; }
        string Name { get; }
        int count { get; }
    }
}