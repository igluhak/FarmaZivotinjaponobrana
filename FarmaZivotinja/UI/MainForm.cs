using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using FarmaZivotinja.Core;
using FarmaZivotinja.Data;

namespace FarmaZivotinja
{
    public class MainForm : Form
    {
        private Farma _farma = new Farma();
        private ListBox _lstZivotinje = null!;
        private Button _btnDodaj = null!;
        private Button _btnStatistika = null!;
        private Button _btnNahrani = null!;
        private Button _btnProizvedi = null!;

        private const string FilePath = "farma.json";

        public MainForm()
        {
            InicijalizirajUI();
            UcitajPodatke();
        }

        private void InicijalizirajUI()
        {
            Text = "Farma životinja";
            Width = 600;
            Height = 400;
            StartPosition = FormStartPosition.CenterScreen;

            _lstZivotinje = new ListBox { Dock = DockStyle.Top, Height = 220 };
            _btnDodaj = new Button { Text = "Dodaj životinju", Left = 10, Top = 230, Width = 120 };
            _btnNahrani = new Button { Text = "Nahrani sve", Left = 150, Top = 230, Width = 120 };
            _btnProizvedi = new Button { Text = "Proizvedi", Left = 290, Top = 230, Width = 120 };
            _btnStatistika = new Button { Text = "Statistika", Left = 430, Top = 230, Width = 120 };

            Controls.AddRange(new Control[] { _lstZivotinje, _btnDodaj, _btnNahrani, _btnProizvedi, _btnStatistika });

            _btnDodaj.Click += BtnDodaj_Click;
            _btnNahrani.Click += BtnNahrani_Click;
            _btnProizvedi.Click += BtnProizvedi_Click;
            _btnStatistika.Click += BtnStatistika_Click;
        }

        private async void UcitajPodatke()
        {
            if (File.Exists(FilePath))
            {
                _farma = await DataManager.UcitajJsonAsync(FilePath);
                OsvjeziPrikaz();
            }
        }

        protected override async void OnFormClosing(FormClosingEventArgs e)
        {
            await DataManager.SpremiJsonAsync(FilePath, _farma);
            base.OnFormClosing(e);
        }

        private void BtnDodaj_Click(object? sender, EventArgs e)
        {
            using var f = new AddAnimalForm();
            if (f.ShowDialog() == DialogResult.OK && f.NovaZivotinja != null)
            {
                _farma.Dodaj(f.NovaZivotinja);
                OsvjeziPrikaz();
            }
        }

        private void BtnNahrani_Click(object? sender, EventArgs e)
        {
            _farma.NahraniSve();
            OsvjeziPrikaz();
            MessageBox.Show("Sve životinje su nahranjene!", "Info");
        }

        private void BtnProizvedi_Click(object? sender, EventArgs e)
        {
            _farma.Proizvodnja();
            OsvjeziPrikaz();
            var proizvodi = string.Join(", ", _farma.SkladisteProizvoda.Select(kv => $"{kv.Key}: {kv.Value}"));
            MessageBox.Show($"Skladiste proizvoda: {proizvodi}", "Proizvodnja");
        }

        private void BtnStatistika_Click(object? sender, EventArgs e)
        {
            using var s = new StatistikaForm(_farma);
            s.ShowDialog();
        }

        private void OsvjeziPrikaz()
        {
            _lstZivotinje.Items.Clear();
            foreach (var z in _farma.DohvatiSve())
                _lstZivotinje.Items.Add(z.Opis());
        }
    }
}
