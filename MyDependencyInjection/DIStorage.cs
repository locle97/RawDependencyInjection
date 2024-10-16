public class DependencyContainer
{
    private readonly List<DIModel> _dependencies;

    public DependencyContainer()
    {
        _dependencies = new();
    }

    public void AddType(Type t)
    {
        _dependencies.Add(new DIModel(t));
    }

    public void AddType<T>()
    {
        _dependencies.Add(new DIModel(typeof(T)));
    }

    public void AddType<TAbs, TImp>()
    {
        _dependencies.Add(new DIModel(typeof(TAbs), typeof(TImp)));
    }

    public Type GetImplementation(Type abs)
    {
         DIModel dependency = _dependencies.Single(t => t.Abstraction!.Name == abs.Name);
         return dependency.Implementation!;
    }
}

