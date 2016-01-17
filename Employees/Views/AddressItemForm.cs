using System;
using System.Windows.Forms;

namespace Employees.Views
{
    public partial class AddressItemForm : Form, IAddressAddEditView
    {
        public AddressItemForm()
        {
            InitializeComponent();
        }

        public ComboBox CityComboBox { get { return comboBoxCity; } }
        public ComboBox StreetComboBox { get { return comboBoxStreet; } }
        public string House
        {
            get { return textBoxHouse.Text.Trim(); }
            set { textBoxHouse.Text = value; }
        }

        public event EventHandler Loaded;

        private void LoadedHandler(object sender, EventArgs e)
        {
            if (Loaded != null)
                Loaded(this, EventArgs.Empty);
        }

        public event EventHandler CitySelected;

        private void CitySelectedHandler(object sender, EventArgs e)
        {
            if (CitySelected != null)
                CitySelected(this, EventArgs.Empty);
        }

        public event EventHandler StreetSelected;

        private void StreetSelectedHandler(object sender, EventArgs e)
        {
            if (StreetSelected != null)
                StreetSelected(this, EventArgs.Empty);
        }

        public event EventHandler AddCity;

        private void AddCityHandler(object sender, EventArgs e)
        {
            if (AddCity != null)
                AddCity(this, EventArgs.Empty);
        }

        public event EventHandler AddStreet;

        private void AddStreetHandler(object sender, EventArgs e)
        {
            if (AddStreet != null)
                AddStreet(this, EventArgs.Empty);
        }

        private void textBoxHouse_TextChanged(object sender, EventArgs e)
        {
            buttonOk.Enabled = House.Length > 0;
        }
    }
}