namespace APBD_Cw2;

public class Rental
{
    public User Renter { get; set; }
    public Equipment RentedItem { get; set; }
    public DateTime RentDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? ReturnDate { get; set; }

    public Rental(User renter, Equipment item, int daysToRent)
    {
        Renter = renter;
        RentedItem = item;
        RentDate = DateTime.Now;
        DueDate = DateTime.Now.AddDays(daysToRent);
        ReturnDate = null;
    }

    public bool IsActive()
    {
        return ReturnDate == null;
    }
}