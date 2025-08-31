using System;
using System.Linq;
using System.Windows.Forms;
using FarmaZivotinja.Core;
using FarmaZivotinja.Data;

namespace FarmaZivotinja
{
    public class MainForm : Form
    {
        private readonly Farma _farma = new Farma();
        private ListBox _lstZivotinje = null!;
        private Button _btnDodaj = null!;
        private Button _btnStatistika = null!;

        public MainForm()
        {
            InicijalizirajUI();
            UcitajPodatkeIzBaze();
        }

        private void InicijalizirajUI()
        {
            Text = "Farma životinja";
            Width = 600;
            Height = 400;
            StartPosition = FormStartPosition.CenterScreen;

            _lstZivotinje = new ListBox { Dock = DockStyle.Top, Height = 220 };
            _btnDodaj = new Button { Text = "Dodaj životinju", Dock = DockStyle.Left, Width = 150 };
            _btnStatistika = new Button { Text = "Statistika", Dock = DockStyle.Right, Width = 150 };

            Controls.Add(_lstZivotinje);
            Controls.Add(_btnDodaj);
            Controls.Add(_btnStatistika);

            _btnDodaj.Click += BtnDodaj_Click;
            _btnStatistika.Click += BtnStatistika_Click;
        }

        private void UcitajPodatkeIzBaze()
        {
            try
            {
                using var db = new FarmDbContext();
                var lista = db.Animals.ToList();

                foreach (var a in lista)
                {
                    // mapiraj species u konkretnu klasu
                    var z = ZivotinjaFactory.Create(a.Species, a.Name, a.Age, a.WeightKg);
                    _farma.Dodaj(z);
                }

                OsvjeziPrikaz();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška pri čitanju iz baze: " + ex.Message);
            }
        }

        private void OsvjeziPrikaz()
        {
            _lstZivotinje.Items.Clear();
            foreach (var z in _farma.DohvatiSve())
            {
                _lstZivotinje.Items.Add(z);
            }
        }

        private void BtnDodaj_Click(object? sender, EventArgs e)
        {
            using var f = new AddAnimalForm();
            if (f.ShowDialog() == DialogResult.OK && f.NovaZivotinja != null)
            {
                _farma.Dodaj(f.NovaZivotinja);

                try
                {
                    using var db = new FarmDbContext();
                    db.Animals.Add(new AnimalEntity
                    {
                        Name = f.NovaZivotinja.Ime,
                        Age = f.NovaZivotinja.Dob,
                        WeightKg = f.NovaZivotinja.TezinaKg,
                        Species = f.NovaZivotinja.Vrsta
                    });
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Greška pri spremanju u bazu: " + ex.Message);
                }

                OsvjeziPrikaz();
            }
        }

        private void BtnStatistika_Click(object? sender, EventArgs e)
        {
            using var s = new StatistikaForm(_farma);
            s.ShowDialog();
        }
    }
}
