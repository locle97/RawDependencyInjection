using Dumpify;

public class Repository: IRepository
{
    private int _random = new Random().Next();

    public Repository()
    {
    }

    public Person GetPerson()
    {
        return new Person
        {
            FirstName = $"Loc #{_random}",
            LastName = "Le"
        };
    }
}

