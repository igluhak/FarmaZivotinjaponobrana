using System.Collections.Generic;

namespace FarmaZivotinja.Core
{
    public interface IProizvodac
    {
        string VrstaProizvoda { get; }
        int Kolicina { get; }
        void Proizvedi(Dictionary<string, int> skladisteProizvoda);
    }
}
