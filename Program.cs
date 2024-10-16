internal class Program
{
    private static void Main(string[] args)
    {
        DependencyContainer container = new();
        container.AddType<IRepository, Repository>();
        container.AddType<IService, Service>();
        container.AddType<Controller>();

        DependencyProvider provider = new(container);
        Controller controller = provider.CreateInstance<Controller>();

        controller.Render();
    }
}
