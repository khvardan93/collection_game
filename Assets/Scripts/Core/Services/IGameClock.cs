namespace Core.Services
{
    public interface IGameClock
    {
        float Time { get; }
        void Tick(float deltaTime);
    }
}
