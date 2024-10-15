public class DIModel
{
    private Type? _abstration;
    private Type? _implementation;

    public Type? Abstraction => _abstration;
    public Type? Implementation => _implementation;

    public DIModel(Type t)
    {
        _abstration = t;
        _implementation = t;
    }

    public DIModel(Type abs, Type imp)
    {
        _abstration = abs;
        _implementation = imp;
    }
}

