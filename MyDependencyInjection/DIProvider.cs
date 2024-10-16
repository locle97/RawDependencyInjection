using System.Reflection;

public class DependencyProvider
{
    private DependencyContainer _storage;

    public DependencyProvider(DependencyContainer storage)
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

