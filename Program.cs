internal class Program
{
    private static void Main(string[] args)
    {
        DIStorage storage = new();
        storage.AddType<IRepository, Repository>();
        storage.AddType<IService, Service>();
        storage.AddType<Controller>();

        DIProvider serviceProvider = new(storage);
        Controller controller = serviceProvider.CreateInstance<Controller>();

        controller.Render();
    }
}
