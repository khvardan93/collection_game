namespace Core.DI
{
    public interface IContainer
    {
        void Register<TAbstraction, TImplementation>() where TImplementation : TAbstraction, new();
        void Register<TAbstraction>(TAbstraction instance);
        T Resolve<T>();
        void Destroy();
    }
}
