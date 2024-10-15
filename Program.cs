using System.Reflection;
using Dumpify;

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

public class DIProvider
{
    private DIStorage _storage;

    public DIProvider(DIStorage storage)
    {
        _storage = storage;
    }

    public object CreateInstance(Type t)
    {
        Type implementation = _storage.GetImplementation(t);
        ConstructorInfo constructor = implementation.GetConstructors().Single();
        ParameterInfo[] parameters = constructor.GetParameters();

        List<object> listDependency = new();
        foreach (var param in parameters)
        {
            listDependency.Add(CreateInstance(param.ParameterType));
        }

        return Activator.CreateInstance(implementation, listDependency.ToArray())!;
    }

    public T CreateInstance<T>() => (T) CreateInstance(typeof(T));
}

public class DIStorage
{
    private readonly List<DI> _dependencies;

    public DIStorage()
    {
        _dependencies = new();
    }

    public void AddType(Type t)
    {
        _dependencies.Add(new DI(t));
    }

    public void AddType<T>()
    {
        _dependencies.Add(new DI(typeof(T)));
    }

    public void AddType<TAbs, TImp>()
    {
        _dependencies.Add(new DI(typeof(TAbs), typeof(TImp)));
    }

    public Type GetImplementation(Type abs)
    {
         DI dependency = _dependencies.Single(t => t.Abstraction!.Name == abs.Name);
         return dependency.Implementation!;
    }
}

public class DI
{
    private Type? _abstration;
    private Type? _implementation;

    public Type? Abstraction => _abstration;
    public Type? Implementation => _implementation;

    public DI(Type t)
    {
        _abstration = t;
        _implementation = t;
    }

    public DI(Type abs, Type imp)
    {
        _abstration = abs;
        _implementation = imp;
    }
}

public class Controller
{
    private readonly IService _service;

    public Controller(IService service)
    {
        _service = service;
    }

    public void Render()
    {
        string name = _service.GetFullname();
        $"Hello {name}!".Dump();
    }
}

public class Service: IService
{
    private readonly IRepository _repository;

    public Service(IRepository repository)
    {
        _repository = repository;
    }

    public string GetFullname()
    {
        Person person = _repository.GetPerson();
        return person.FirstName + " " + person.LastName;
    }
}

public interface IService
{
    string GetFullname();
}

public class Repository: IRepository
{
    public Person GetPerson()
    {
        return new Person
        {
            FirstName = "Loc",
            LastName = "Le"
        };
    }
}

public interface IRepository
{
    Person GetPerson();
}

public class Person
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}
