using System;
using APBD_Cw2;

class Program 
{
    static void Main(string[] args)
    {
        
        RentalManager manager = new RentalManager();
        
        Console.WriteLine("\n11 & 12 ---Dodawanie sprzętu i użytkowników---");

        Laptop laptop1 = new Laptop("Macbook Pro", "macOS", 32);
        Laptop laptop2 = new Laptop("Thinkpad T14", "Linux", 16);
        Camera camera1 = new Camera("Sony A7", true, 24);
        
        manager.AddEquipment(laptop1);
        manager.AddEquipment(laptop2);
        manager.AddEquipment(camera1);

        Student student = new Student("Jan", "Kowalski", "s12345");
        Student student2 = new Student("Piotr", "Kubek", "s54321");
        Employee pracownik = new Employee("Anna", "Nowak", "Wydział IT");
        
        manager.AddUser(student);
        manager.AddUser(pracownik);
        
        manager.PrintSystemReport();
        
        
        Console.WriteLine("\n13 ---Poprawne Wypożyczenie---");

        Rental rentalLaptop = manager.RentEquipment(student, laptop1, 7);
        Rental rentalCamera = manager.RentEquipment(student, camera1, 3);
        
        
        Console.WriteLine("\n14 ---Niepoprawne Operacje---");

        try
        {
            manager.RentEquipment(pracownik, laptop1, 5);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd: {ex.Message}");
        }

        try
        {
            manager.RentEquipment(student, laptop2, 2);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd: {ex.Message}");
        }
        
        
        Console.WriteLine("\n15 ---Zwrot w Terminie---");
        
        manager.ReturnEquipment(rentalLaptop);
        
        
        Console.WriteLine("\n16 ---Opóźniony Zwrot z Karą---");

        DateTime fakeLateDate = rentalCamera.RentDate.AddDays(10);
        manager.ReturnEquipment(rentalCamera, fakeLateDate);
        
        
        Console.WriteLine("\n17 ---Raport Końcowy---");
        
        manager.PrintSystemReport();
        
        
        
        
        manager.PrintAllEquipment();
    }
}