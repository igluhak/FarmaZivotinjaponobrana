using System;
using System.Collections.Generic;

namespace FarmaZivotinja.Core
{
    // Apstraktna klasa - svi tipovi životinja je nasljeđuju
    public abstract class Zivotinja
    {
        public string Ime { get; set; }
        public int Dob { get; set; }
        public double TezinaKg { get; set; }

        public abstract string Vrsta { get; }

        protected Zivotinja(string ime, int dob, double tezinaKg)
        {
            if (string.IsNullOrWhiteSpace(ime))
                throw new FarmException("Ime životinje ne može biti prazno!");

            if (dob < 0)
                throw new FarmException("Dob životinje mora biti pozitivna!");

            if (tezinaKg <= 0)
                throw new FarmException("Težina životinje mora biti veća od 0!");

            Ime = ime;
            Dob = dob;
            TezinaKg = tezinaKg;
        }

        public virtual string Opis()
        {
            return $"{Vrsta} - {Ime}, {Dob} god., {TezinaKg} kg";
        }

        public override string ToString() => Opis();
    }

   
    public class Krava : Zivotinja, IProizvodac
    {
        public override string Vrsta => nameof(Krava);

        public Krava(string ime, int dob, double tezinaKg) : base(ime, dob, tezinaKg) { }

        public string VrstaProizvoda => "Mlijeko";
        public int Kolicina => 5;

        public void Proizvedi(Dictionary<string, int> skladisteProizvoda)
        {
            if (!skladisteProizvoda.ContainsKey(VrstaProizvoda))
                skladisteProizvoda[VrstaProizvoda] = 0;

            skladisteProizvoda[VrstaProizvoda] += Kolicina;
        }
    }

    
    public class Ovca : Zivotinja, IProizvodac
    {
        public override string Vrsta => nameof(Ovca);

        public Ovca(string ime, int dob, double tezinaKg) : base(ime, dob, tezinaKg) { }

        public string VrstaProizvoda => "Vuna";
        public int Kolicina => 2;

        public void Proizvedi(Dictionary<string, int> skladisteProizvoda)
        {
            if (!skladisteProizvoda.ContainsKey(VrstaProizvoda))
                skladisteProizvoda[VrstaProizvoda] = 0;

            skladisteProizvoda[VrstaProizvoda] += Kolicina;
        }
    }

    
    public class Kokos : Zivotinja, IProizvodac
    {
        public override string Vrsta => nameof(Kokos);

        public Kokos(string ime, int dob, double tezinaKg) : base(ime, dob, tezinaKg) { }

        public string VrstaProizvoda => "Jaja";
        public int Kolicina => 3;

        public void Proizvedi(Dictionary<string, int> skladisteProizvoda)
        {
            if (!skladisteProizvoda.ContainsKey(VrstaProizvoda))
                skladisteProizvoda[VrstaProizvoda] = 0;

            skladisteProizvoda[VrstaProizvoda] += Kolicina;
        }
    }

    
    public class Svinja : Zivotinja
    {
        public override string Vrsta => nameof(Svinja);
        public Svinja(string ime, int dob, double tezinaKg) : base(ime, dob, tezinaKg) { }
    }

    
    public class Konj : Zivotinja
    {
        public override string Vrsta => nameof(Konj);
        public Konj(string ime, int dob, double tezinaKg) : base(ime, dob, tezinaKg) { }
    }

    
    public class OpstaZivotinja : Zivotinja
    {
        private readonly string _vrsta;

        public override string Vrsta => _vrsta;

        public OpstaZivotinja(string vrsta, string ime, int dob, double tezinaKg) : base(ime, dob, tezinaKg)
        {
            _vrsta = vrsta ?? "Nepoznato";
        }
    }

    
    public static class ZivotinjaFactory
    {
        public static Zivotinja Create(string vrsta, string ime, int dob, double tezinaKg)
        {
            return vrsta switch
            {
                nameof(Krava) or "Krava" => new Krava(ime, dob, tezinaKg),
                nameof(Ovca) or "Ovca" => new Ovca(ime, dob, tezinaKg),
                nameof(Kokos) or "Kokos" => new Kokos(ime, dob, tezinaKg),
                nameof(Svinja) or "Svinja" => new Svinja(ime, dob, tezinaKg),
                nameof(Konj) or "Konj" => new Konj(ime, dob, tezinaKg),
                _ => new OpstaZivotinja(vrsta, ime, dob, tezinaKg)
            };
        }
    }
}
