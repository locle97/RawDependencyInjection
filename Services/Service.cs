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

