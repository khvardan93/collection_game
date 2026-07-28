namespace Core.DI
{
    public static class ServiceLocator
    {
        public static IContainer Container { get; set; }

        public static void Reset()
        {
            Container = null;
        }
    }
}
