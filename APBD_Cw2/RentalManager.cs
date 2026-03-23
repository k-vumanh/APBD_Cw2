using System.Xml;

namespace APBD_Cw2;

public class RentalManager
{
    private List<Equipment> _equipment = new List<Equipment>();
    private List<Rental> _rentals = new List<Rental>();
    private List<User> _users = new List<User>();

    private const double DailyPenaltyRate = 10.0;

    
    public void AddEquipment(Equipment item)
    {
        _equipment.Add(item);
    } 
    
    public void AddUser(User user)
    {
        _users.Add(user);
    }

    public Rental RentEquipment(User user, Equipment item, int days)
    {
        if (item.Status != EquipmentStatus.Available)
        {
            throw new InvalidOperationException($"Błąd: Sprzęt '{item.Name}' jest niedostępny.");
        }

        int activeRentalsCount = 0;
        foreach (Rental r in _rentals)
        {
            if (r.Renter.Id == user.Id && r.IsActive())
            {
                activeRentalsCount++;
            }
           
        }
        if (activeRentalsCount >= user.MaxRentals)
        {
            throw new InvalidOperationException(
                $"Błąd: Użytkownik {user.FirstName} osiągnął limit {user.MaxRentals} wypożyczeń");
        }

        var newRental = new Rental(user, item, days);
        _rentals.Add(newRental);
        item.Status = EquipmentStatus.Rented;
        
        Console.WriteLine($"{user.FirstName} wypożyczył {item.Name}");
        
        return newRental;
    }

    public double ReturnEquipment(Rental rental, DateTime? returnDate = null)
    {
        if (!rental.IsActive())
        {
            throw new InvalidOperationException("To wypożyczenie zostało już zakończone.");
        }
        
        rental.ReturnDate = returnDate ?? DateTime.Now;
        rental.RentedItem.Status = EquipmentStatus.Available;
        
        double penalty = 0;

        if (rental.ReturnDate.Value > rental.DueDate)
        {
            int daysLate = (rental.ReturnDate.Value - rental.DueDate).Days;

            if (daysLate > 0)
            {
                penalty = daysLate *  DailyPenaltyRate;
            }
        }
        
        Console.WriteLine($"Sprzęt {rental.RentedItem.Name} wrócił do wypożyczalni. Kara: {penalty} PLN.");
        return penalty;
    }

    public List<Equipment> GetAvailableEquipment()
    {
        List<Equipment> availableEquipment = new List<Equipment>();
        foreach (Equipment e in _equipment)
        {
            if (e.Status == EquipmentStatus.Available)
            {
                availableEquipment.Add(e);
            }
        }
        
        return availableEquipment;
    }

    public void PrintSystemReport()
    {
        Console.WriteLine("\n ---RAPORT SYSTEMU---");
        Console.WriteLine($"Całkowita liczba sprzętu: {_equipment.Count}");
        Console.WriteLine($"Dostępny sprzęt: {GetAvailableEquipment().Count}");

        int totalActiveRentals = 0;
        foreach (Rental r in _rentals)
        {
            if (r.IsActive())
            {
                totalActiveRentals++;
            }
        }
        Console.WriteLine($"Aktywne wypożyczenia: {totalActiveRentals}");
        Console.WriteLine("------------------------\n");
    }

    public void PrintAllEquipment()
    {
        Console.WriteLine("\n---CAŁY SPRZĘT");
        foreach (Equipment e in _equipment)
        {
            Console.WriteLine($"-{e.Name} (ID: {e.Id}) | Status: {e.Status}");
        }
    }
    
    public void MarkAsUnavailable(Equipment item)
    {
        if (item.Status != EquipmentStatus.Rented)
        {
            throw new InvalidOperationException("Nie można oznaczyć wypożyczonego jako zesputego.");
        }
        item.Status = EquipmentStatus.Unavailable;
        Console.WriteLine($"Sprzęt {item.Name} został oznaczony jako niedostepny.");
    }

    public void PrintUserActiveRentals(User user)
    {
        Console.WriteLine($"\n---AKTYWNE WYPOŻYCZENIA: {user.FirstName} {user.LastName}---");
        foreach (Rental r in _rentals)
        {
            if (r.Renter.Id == user.Id && r.IsActive())
            {
                Console.WriteLine($"- {r.RentedItem.Name} (Wypożyczono: {r.RentDate.ToShortDateString()}, Termin: {r.DueDate.ToShortDateString()})");
            }
        }
    }

    public void PrintOverdueRentals()
    {
        Console.WriteLine("\n--- PRZETERMINOWANE WYPOŻYCZENIA ---");
        foreach (Rental r in _rentals)
        {
            if (r.IsActive() && DateTime.Now > r.DueDate)
            {
                Console.WriteLine($"- {r.Renter.FirstName} przetrzymuje {r.RentedItem.Name}! (Termin: {r.DueDate.ToShortDateString()})");
            }
        }
    }
}