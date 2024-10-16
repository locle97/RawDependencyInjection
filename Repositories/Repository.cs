using Dumpify;

public class Repository: IRepository
{
    private readonly MyConfig _config;
    private int _random = new Random().Next();

    public Repository(MyConfig config)
    {
        _config = config;
    }

    public Person GetPerson()
    {
        $"Getting data from {_config.ConnectionString}".Dump();
        return new Person
        {
            FirstName = $"Loc #{_random}",
            LastName = "Le"
        };
    }
}

