public class Service: IService
{
    private readonly IRepository _repository;
    private readonly MyConfig _config;

    public Service(IRepository repository, MyConfig config)
    {
        _repository = repository;
        _config = config;
    }

    public string GetFullname()
    {
        Person person = _repository.GetPerson();
        return person.FirstName + " " + person.LastName;
    }
}

