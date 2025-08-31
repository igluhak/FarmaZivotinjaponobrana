using System;
using System.Windows.Forms;
using FarmaZivotinja.Core;

namespace FarmaZivotinja
{
    [Serializable]
    public class AddAnimalForm : Form
    {
        private TextBox _txtIme = null!;
        private NumericUpDown _numDob = null!;
        private NumericUpDown _numTezina = null!;
        private ComboBox _cmbVrsta = null!;
        private Button _btnSpremi = null!;

        [field: NonSerialized]
        public Zivotinja? NovaZivotinja { get; private set; }

        // Rest of the code remains unchanged
    }
}
