namespace APBD_Cw2;

public abstract class Equipment
{
    public Guid Id { get; protected set; }
    public string Name { get; set; }
    public EquipmentStatus Status { get; set; }

    protected Equipment(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
        Status = EquipmentStatus.Available;
    }
    
    
}