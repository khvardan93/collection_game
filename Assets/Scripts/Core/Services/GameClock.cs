namespace Core.Services
{
    public sealed class GameClock : IGameClock
    {
        public float Time { get; private set; }

        public void Tick(float deltaTime)
        {
            Time += deltaTime;
        }
    }
}
