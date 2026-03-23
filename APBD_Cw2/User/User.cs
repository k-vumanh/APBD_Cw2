namespace APBD_Cw2;

public abstract class User
{
    public Guid Id  { get; protected set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public abstract int MaxRentals { get; }
    protected User(string firstName, string lastName)
    {
        Id = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
    }
    
    
}