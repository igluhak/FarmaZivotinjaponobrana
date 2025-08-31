using System.Linq;
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

            var lbl = new Label
            {
                Text = $"Ukupno životinja: {svi.Count}\nProsječna težina: {prosjekTezine:F2} kg",
                Dock = DockStyle.Fill,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            };

            Controls.Add(lbl);
        }
    }
}
