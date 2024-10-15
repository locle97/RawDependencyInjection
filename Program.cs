using Dumpify;

internal class Program
{
    private static void Main(string[] args)
    {
        Repository repo = new Repository();
        Service service = new Service(repo);
        Controller controller = new Controller(service);

        controller.Render();
    }
}

public class Controller
{
    private readonly Service _service;

    public Controller(Service service)
    {
        _service = service;
    }

    public void Render()
    {
        string name = _service.GetName();
        $"Hello {name}!".Dump();
    }
}

public class Service
{
    private readonly Repository _repository;

    public Service(Repository repository)
    {
        _repository = repository;
    }

    public string GetName()
    {
        return _repository.FetchName();
    }
}

public class Repository
{
    public string FetchName()
    {
        return "Loc";
    }
}
