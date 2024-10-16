using System.Reflection;

public class DependencyProvider
{
    private DependencyContainer _container;

    public DependencyProvider(DependencyContainer container)
    {
        _container = container;
    }

    public object CreateInstance(Type t)
    {
        DependencyModel dependency = _container.GetDependency(t);
        LifeSpan lifeSpan = dependency.LifeSpan;

        if (lifeSpan == LifeSpan.Singleton && dependency.Instance is not null)
            return dependency.Instance;

        Type implementation = dependency.Implementation!;
        ConstructorInfo constructor = implementation.GetConstructors().Single();
        ParameterInfo[] parameters = constructor.GetParameters();

        List<object> listDependency = new();
        foreach (var param in parameters)
        {
            listDependency.Add(CreateInstance(param.ParameterType));
        }

        object instance = Activator.CreateInstance(implementation,
                                                   listDependency.ToArray())!;

        if (lifeSpan == LifeSpan.Singleton && dependency.Instance is null)
            dependency.Instance = instance;

        return instance;
    }

    public T CreateInstance<T>() => (T)CreateInstance(typeof(T));
}

