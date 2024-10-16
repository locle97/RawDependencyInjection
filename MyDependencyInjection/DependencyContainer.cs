public class DependencyContainer
{
    private readonly List<DependencyModel> _dependencies = new();

    public DependencyContainer()
    {
    }

    private void AddType(Type abs, Type imp, LifeSpan lifeSpan)
    {
        _dependencies.Add(new DependencyModel(abs, imp, lifeSpan));
    }

    private void AddType(Type abs, Type imp, LifeSpan lifeSpan, object instance)
    {
        _dependencies.Add(new DependencyModel(abs, imp, lifeSpan, instance));
    }

#region Transient
    public void AddTransient(Type t) => AddType(t, t, LifeSpan.Transient);

    public void AddTransient<T>() => AddTransient(typeof(T));

    public void AddTransient<T>(object initialInstance) => AddType(typeof(T), typeof(T), LifeSpan.Transient, initialInstance);

    public void AddTransient(Type abstraction, Type implementation) => AddType(abstraction, implementation, LifeSpan.Transient);

    public void AddTransient<TAbstraction, TImplementation>() => AddTransient(typeof(TAbstraction), typeof(TImplementation));

    public void AddTransient<TAbstraction>(Type implementation) => AddTransient(typeof(TAbstraction), implementation);
#endregion

#region Singleton
    public void AddSingleton(Type t) => AddType(t, t, LifeSpan.Singleton);

    public void AddSingleton<T>() => AddSingleton(typeof(T));

    public void AddSingleton<T>(object initialInstance) => AddType(typeof(T), typeof(T), LifeSpan.Singleton, initialInstance);

    public void AddSingleton(Type abstraction, Type implementation) => AddType(abstraction, implementation, LifeSpan.Singleton);

    public void AddSingleton<TAbstraction, TImplementation>() => AddSingleton(typeof(TAbstraction), typeof(TImplementation));

    public void AddSingleton<TAbstraction>(Type implementation) => AddSingleton(typeof(TAbstraction), implementation);
#endregion

    public DependencyModel GetDependency(string abstractionName)
    {
        DependencyModel dependency = _dependencies.Single(t => t.Abstraction!.Name == abstractionName);
        return dependency;
    }

    public Type GetImplementation(Type abs) => GetDependency(abs.Name).Implementation!;

    public Type GetImplementation(string abstractionName) => GetDependency(abstractionName).Implementation!;

    public DependencyModel GetDependency(Type abs) => GetDependency(abs.Name);
}

