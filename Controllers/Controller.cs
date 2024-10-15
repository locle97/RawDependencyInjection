using Dumpify;

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

