public class DependencyModel
{
    private Type? _abstration;
    private Type? _implementation;

    public Type? Abstraction => _abstration;
    public Type? Implementation => _implementation;

    private LifeSpan _lifeSpan = LifeSpan.Transient;
    private bool _amISingleton => _lifeSpan == LifeSpan.Singleton;
    public LifeSpan LifeSpan => _lifeSpan;

    private object? _instance;
    public object? Instance
    {
        get
        {
            return _instance;
        }
        set
        {
            _instance = value;
        }
    }

    public DependencyModel(Type t,
                           LifeSpan lifeSpan,
                           object? instance = null)
    {
        _abstration = t;
        _implementation = t;
        _lifeSpan = lifeSpan;
        _instance = instance;
    }

    public DependencyModel(Type abs,
                           Type imp,
                           LifeSpan lifeSpan,
                           object? instance = null)
    {
        _abstration = abs;
        _implementation = imp;
        _lifeSpan = lifeSpan;
        _instance = instance;
    }
}
