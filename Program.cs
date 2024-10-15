using System.Reflection;
using Dumpify;

internal class Program
{
    private static void Main(string[] args)
    {
        DIStorage storage = new();
        storage.AddType(typeof(Repository));
        storage.AddType<Service>();
        storage.AddType<Controller>();

        DIProvider provider = new(storage);
        Controller controller = provider.CreateInstance<Controller>();

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
        Type type = _storage.GetType(t);
        ConstructorInfo constructor = type.GetConstructors().Single();
        ParameterInfo[] parameters = constructor.GetParameters();


        List<object> listDependency = new();
        foreach (var param in parameters)
        {
            listDependency.Add(CreateInstance(param.ParameterType));
        }

        return Activator.CreateInstance(type, listDependency.ToArray())!;

    }

    public T CreateInstance<T>() => (T) CreateInstance(typeof(T));
}

public class DIStorage
{
    private readonly List<Type> _types;

    public DIStorage()
    {
        _types = new();
    }

    public void AddType(Type t)
    {
        _types.Add(t);
    }

    public void AddType<T>()
    {
        _types.Add(typeof(T));
    }

    public Type GetType(Type type)
    {
        return _types.Single(t => t.Name == type.Name);
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
        string name = _service.GetFullname();
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

    public string GetFullname()
    {
        Person person = _repository.GetPerson();
        return person.FirstName + " " + person.LastName;
    }
}

public class Repository
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

public class Person
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}
