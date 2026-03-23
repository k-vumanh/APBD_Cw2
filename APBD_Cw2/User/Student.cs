namespace APBD_Cw2;

public class Student : User
{
    public override int MaxRentals => 2;
    
    public string IndexNumber { get; set; }

    public Student(string firstName, string lastName, string index) : base(firstName, lastName)
    {
        IndexNumber = index;
    }
}