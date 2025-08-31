# Farma životinja (C# WinForms, .NET 8)

Ovaj projekt je kostur aplikacije **Farma životinja** koji pokriva kriterije I1–I6 iz uputa.

## Kako pokrenuti
1. Otvori `FarmaZivotinja.sln` u **Visual Studio 2022** (ili noviji).
2. Pri prvom buildanju će se povući NuGet paketi (EF Core, SQLite).
3. Pokreni (F5).

## Što je uključeno
- **Core**: apstraktna klasa `Zivotinja`, izvedene `Krava`, `Ovca`, `Kokos`, sučelje `IProizvod`, prilagođena iznimka `PremaloHraneException`, delegati/događaji (`Ogladnila`, `ProizvodStvoren`), struktura `Pozicija`, unutarnja klasa `Farma.Statistika`, kolekcije `List<>`, `Dictionary<>`.
- **Višedretvenost**: simulacija protoka vremena + async proizvodnja (Task + CancellationToken), zaštita pristupa sa `lock`.
- **UI (WinForms)**: `MainForm` (lista životinja, meni, mreža), `AddAnimalForm`.
- **Data**: JSON serijalizacija (`DataManager`), EF Core `FarmDbContext` (SQLite).
- **Networking**: TCP server/klijent (System.Net.Sockets) koji vraća JSON stanje farme.

## Ideje za nadogradnju (za još bodova)
- Dodaj `StorageForm` za vizualno upravljanje skladištem hrane/proizvoda (progress bar, treeview).
- Implementiraj spremanje/učitavanje u **bazu** (CRUD preko `FarmDbContext` + EF migracije).
- Dodaj `Timer` kontrole u UI i rukovanje događajima tipki (`KeyDown`).
- Dodaj još životinja i različite modele proizvodnje (npr. periodičnost, kvaliteta proizvoda).
- Dodaj sučelja (npr. `ITransport`, `IProdaja`) i dodatne apstraktne klase (npr. `Papkar`).

> Napomena: EF Core dio je pripremljen, ali bez migracija. Ako želiš koristiti bazu:
> - U **Package Manager Console**: `Add-Migration Init` pa `Update-Database`.