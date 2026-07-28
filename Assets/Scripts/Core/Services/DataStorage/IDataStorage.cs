namespace Core.Services
{
    public interface IDataStorage
    {
        void Save<T>(T data) where T : IStorable;
        T Load<T>(string key) where T : IStorable, new();
        bool Exists(string key);
        void Delete(string key);
    }
}
