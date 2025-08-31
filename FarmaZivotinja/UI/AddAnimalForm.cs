using System;
using System.Windows.Forms;
using FarmaZivotinja.Core;

namespace FarmaZivotinja
{
    [Serializable]
    public class AddAnimalForm : Form
    {
        private readonly TextBox _txtIme;
        private readonly NumericUpDown _numDob;
        private readonly NumericUpDown _numTezina;
        private readonly ComboBox _cmbVrsta;
        private readonly Button _btnSpremi;

        [field: NonSerialized]
        public Zivotinja? NovaZivotinja { get; private set; }

        public AddAnimalForm()
        {
            Text = "Dodaj životinju";
            Width = 300;
            Height = 280;
            StartPosition = FormStartPosition.CenterParent;

            _txtIme = new TextBox { Left = 120, Top = 20, Width = 150 };
            _numDob = new NumericUpDown { Left = 120, Top = 60, Width = 150, Minimum = 0, Maximum = 100 };
            _numTezina = new NumericUpDown { Left = 120, Top = 100, Width = 150, Minimum = 1, Maximum = 1000 };
            _cmbVrsta = new ComboBox { Left = 120, Top = 140, Width = 150, DropDownStyle = ComboBoxStyle.DropDownList };
            _btnSpremi = new Button { Text = "Spremi", Left = 120, Top = 180, Width = 100 };

            _cmbVrsta.Items.AddRange(new string[] { "Krava", "Ovca", "Kokos", "Svinja", "Konj" });
            if (_cmbVrsta.Items.Count > 0) _cmbVrsta.SelectedIndex = 0;

            Controls.AddRange(new Control[]
            {
                new Label { Text = "Ime:", Left = 20, Top = 20 },
                _txtIme,
                new Label { Text = "Dob:", Left = 20, Top = 60 },
                _numDob,
                new Label { Text = "Tezina:", Left = 20, Top = 100 },
                _numTezina,
                new Label { Text = "Vrsta:", Left = 20, Top = 140 },
                _cmbVrsta,
                _btnSpremi
            });

            _btnSpremi.Click += BtnSpremi_Click;
        }

        private void BtnSpremi_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_txtIme.Text))
            {
                MessageBox.Show("Unesite ime životinje.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            NovaZivotinja = ZivotinjaFactory.Create(_cmbVrsta.SelectedItem!.ToString()!, _txtIme.Text, (int)_numDob.Value, (double)_numTezina.Value);
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
