using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using FarmaZivotinja.Core;

namespace FarmaZivotinja.Data
{
    public static class DataManager
    {
        private static readonly JsonSerializerOptions Options = new()
        {
            WriteIndented = true
        };

        public static async Task SpremiJsonAsync(string path, Farma farma)
        {
            var dto = DtoMapper.IzFarma(farma);
            await using var fs = File.Create(path);
            await JsonSerializer.SerializeAsync(fs, dto, Options);
        }

        public static async Task<Farma> UcitajJsonAsync(string path)
        {
            await using var fs = File.OpenRead(path);
            var dto = await JsonSerializer.DeserializeAsync<FarmaDto>(fs, Options) ?? new();
            return DtoMapper.UFarma(dto);
        }
    }

    // DTO-i za serijalizaciju
    public class FarmaDto
    {
        public List<ZivotinjaDto> Zivotinje { get; set; } = new();
        public Dictionary<string, int> SkladisteHrane { get; set; } = new();
        public Dictionary<string, int> SkladisteProizvoda { get; set; } = new();
    }

    public class ZivotinjaDto
    {
        public string Vrsta { get; set; } = "";
        public string Ime { get; set; } = "";
        public int Dob { get; set; }
        public double TezinaKg { get; set; }
    }

    public static class DtoMapper
    {
        public static FarmaDto IzFarma(Farma f)
        {
            var dto = new FarmaDto
            {
                SkladisteHrane = new(f.SkladisteHrane),
                SkladisteProizvoda = new(f.SkladisteProizvoda)
            };
            foreach (var z in f.Zivotinje)
            {
                dto.Zivotinje.Add(new ZivotinjaDto
                {
                    Vrsta = z.Vrsta,
                    Ime = z.Ime,
                    Dob = z.Dob,
                    TezinaKg = z.TezinaKg
                });
            }
            return dto;
        }

        public static Farma UFarma(FarmaDto dto)
        {
            var f = new Farma();
            foreach (var z in dto.Zivotinje)
            {
                Core.Zivotinja obj = ZivotinjaFactory.Create(z.Vrsta, z.Ime, z.Dob, z.TezinaKg);
                if (obj != null) f.Dodaj(obj);
            }
            foreach (var kv in dto.SkladisteHrane) f.SkladisteHrane[kv.Key] = kv.Value;
            foreach (var kv in dto.SkladisteProizvoda) f.SkladisteProizvoda[kv.Key] = kv.Value;
            return f;
        }
    }
}
