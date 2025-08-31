using System.Collections.Generic;

namespace FarmaZivotinja.Core
{
   
    public class FarmContainer<T>
    {
        private readonly List<T> items = new();

        public void Dodaj(T item) => items.Add(item);
        public IEnumerable<T> DohvatiSve() => items;
        public int BrojStavki => items.Count;
    }
}
