namespace APBD_Cw2;

public class Laptop : Equipment
{
    public string OperatingSystem { get; set; }
    public int RamSize { get; set; }
    
    public Laptop(string name, string os, int ram) : base(name)
    {
        OperatingSystem = os;
        RamSize = ram;
    }
    
}