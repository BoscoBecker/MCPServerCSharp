namespace MCPServer.Service;

public static class ServiceLocator
{
    public static IServiceProvider? Instance { get; private set; }

    public static void SetProvider(IServiceProvider provider)
    {
        Instance = provider;
    }
}
