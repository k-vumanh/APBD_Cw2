namespace APBD_Cw2;

public class Employee : User
{
    public override int MaxRentals => 5;
    
    public string Departament { get; set; }

    public Employee(string firstName, string lastName, string departament) : base(firstName, lastName)
    {
        Departament = departament;
    }
}