using System.Linq;
using System.Text;
using System.Windows.Forms;
using FarmaZivotinja.Core;

namespace FarmaZivotinja
{
    public class StatistikaForm : Form
    {
        private readonly Farma _farma;

        public StatistikaForm(Farma farma)
        {
            _farma = farma;
            InicijalizirajUI();
        }

        private void InicijalizirajUI()
        {
            Text = "Statistika";
            Width = 400;
            Height = 300;
            StartPosition = FormStartPosition.CenterParent;

            var svi = _farma.DohvatiSve().ToList();
            var prosjekTezine = svi.Any() ? svi.Average(z => z.TezinaKg) : 0.0;

            // Prikaz skladišta proizvoda
            var skladisteBuilder = new StringBuilder();
            if (_farma.SkladisteProizvoda.Any())
            {
                foreach (var kv in _farma.SkladisteProizvoda)
                {
                    skladisteBuilder.AppendLine($"{kv.Key}: {kv.Value}");
                }
            }
            else
            {
                skladisteBuilder.AppendLine("Skladište proizvoda je prazno.");
            }

            var lbl = new Label
            {
                Text = $"Ukupno životinja: {svi.Count}\n" +
                       $"Prosječna težina: {prosjekTezine:F2} kg\n\n" +
                       $"Skladište proizvoda:\n{skladisteBuilder}",
                Dock = DockStyle.Fill,
                TextAlign = System.Drawing.ContentAlignment.TopLeft
            };

            Controls.Add(lbl);
        }
    }
}
