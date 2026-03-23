#Uruchomienie projektu
Aby uruchomić projekt, otwórz terminal w folderze głównym i wpisz:
`dotnet run`

#Decyzje projektowe i architektura

1. Kohezja (Spójność) i Odpowiedzialność
- Modele Domenowe (`User`, `Equipment`, `Rental`) są odpowiedzialne WYŁĄCZNIE za przechowywanie własnego stanu. Nie decydują o tym, czy można coś wypożyczyć.
- `RentalManager` posiada wysoką kohezję – odpowiada wyłącznie za logikę biznesową wypożyczalni. Skupia się na weryfikacji limitów, statusów i przeliczaniu kar.
- Limity wypożyczeń nie są zaszyte w menedżerze za pomocą mnóstwa instrukcji `if (user.Type == "Student")`. Zamiast tego wykorzystano **polimorfizm** – klasa bazowa `User` definiuje abstrakcyjną właściwość `MaxRentals`, którą podklasy (`Student`, `Employee`) same implementują. Dzięki temu menedżer nie musi wiedzieć, z kim rozmawia, interesuje go tylko polimorficzny limit.

2. Zmniejszenie Couplingu (Sprzężenia)
- Klasa `RentalManager` pracuje na abstraktach (`Equipment`, `User`), a nie na konkretnych implementacjach (`Laptop`, `Student`). Dzięki temu, jeśli w przyszłości dodamy nowa klasę, logika menedżera nie będzie wymagała żadnych zmian.
- Ręczne filtrowanie danych (np. pętle `foreach` z if'ami) zostało zamknięte i ukryte w metodach takich jak `PrintUserActiveRentals()`, co sprawia, że reszta systemu nie musi znać wewnętrznej struktury kolekcji `_rentals`.

3. Obsługa błędów
Operacje, które nie mogą zostać zrealizowane (np. próba wypożyczenia przez studenta trzeciego sprzętu), nie zwracają milcząco wartości `null`. Zamiast tego jawnie rzucają wyjątek `InvalidOperationException` z jasnym komunikatem, co ułatwia zarządzanie przepływem programu w klasie wywołującej (`Program.cs`).
