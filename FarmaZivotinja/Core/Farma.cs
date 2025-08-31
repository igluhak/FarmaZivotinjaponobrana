using System;
using System.Collections.Generic;
using System.Linq;

namespace FarmaZivotinja.Core
{
    public class Farma
    {
        private readonly object _lock = new();

        public List<Zivotinja> Zivotinje { get; private set; } = new();
        public Dictionary<string, int> SkladisteHrane { get; private set; } = new();
        public Dictionary<string, int> SkladisteProizvoda { get; private set; } = new();

        public void Dodaj(Zivotinja zivotinja)
        {
            if (zivotinja == null)
                throw new FarmException("Životinja ne može biti bez podataka");

            lock (_lock)
            {
                Zivotinje.Add(zivotinja);
            }
        }

       
        internal void DodajZivotinju(Zivotinja zivotinja)
        {
            Dodaj(zivotinja);
        }

        public void Ukloni(Zivotinja zivotinja)
        {
            lock (_lock)
            {
                Zivotinje.Remove(zivotinja);
            }
        }

        public IEnumerable<Zivotinja> DohvatiSve()
        {
            lock (_lock)
            {
                return Zivotinje.ToList(); // vraća kopiju
            }
        }

        public void NahraniSve()
        {
        }

        public void Proizvodnja()
        {
            lock (_lock)
            {
                foreach (var p in Zivotinje.OfType<IProizvodac>())
                {
                    p.Proizvedi(SkladisteProizvoda);
                }
            }
        }

        public int BrojZivotinja
        {
            get
            {
                lock (_lock) return Zivotinje.Count;
            }
        }
    }
}
