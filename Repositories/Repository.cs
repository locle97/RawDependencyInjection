public class Repository: IRepository
{
    public Person GetPerson()
    {
        return new Person
        {
            FirstName = "Loc",
            LastName = "Le"
        };
    }
}

