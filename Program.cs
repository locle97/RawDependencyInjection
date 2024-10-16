internal class Program
{
    private static void Main(string[] args)
    {
        DependencyContainer container = new();

        container.AddTransient<IRepository, Repository>();
        container.AddTransient<IService, Service>();
        container.AddTransient<Controller>();

        DependencyProvider provider = new(container);
        Controller controller1 = provider.CreateInstance<Controller>();
        Controller controller2 = provider.CreateInstance<Controller>();
        Controller controller3 = provider.CreateInstance<Controller>();

        controller1.Render();
        controller2.Render();
        controller3.Render();
    }
}

public class MyConfig
{
    public string ConnectionString { get; set; } = string.Empty;
    public int RetryCount { get; set; } = 0;
}
