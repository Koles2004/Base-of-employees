using System;
using System.Windows.Forms;

namespace Employees.Views
{
    public partial class EmployeeItemForm : Form, IEmployeeAddEditView
    {
        public EmployeeItemForm()
        {
            InitializeComponent();
        }

        public ComboBox ComboBoxSex { get { return comboBoxSex; } }
        public ComboBox PositionComboBox { get { return comboBoxPosition; } }
        public ComboBox AddressComboBox { get { return comboBoxAddress; } }
        public PictureBox PictureBoxPhoto { get { return pictureBoxPhoto; } }
        public Button ButtonDeletePhoto { get { return buttonDeletePhoto; } }

        public string GroupBoxText { set { groupBox1.Text = value; } }
        
        public string Surname
        {
            get { return textBoxSurname.Text.Trim(); }
            set { textBoxSurname.Text = value; }
        }

        public string NameOfEmployee
        {
            get { return textBoxName.Text.Trim(); }
            set { textBoxName.Text = value; }
        }

        public string Patronymic
        {
            get { return textBoxPatronymic.Text.Trim(); }
            set { textBoxPatronymic.Text = value; }
        }

        public ComboBox Sex { get { return comboBoxSex; } }

        public string Phone
        {
            get { return textBoxPhone.Text.Trim(); }
            set { textBoxPhone.Text = value; }
        }

        public DateTime DateOfBirth
        {
            get { return dateTimePicker1.Value; }
            set { dateTimePicker1.Value = value; }
        }

        public event EventHandler Loaded;

        private void LoadedHandler(object sender, EventArgs e)
        {
            if (Loaded != null)
                Loaded(this, EventArgs.Empty);
        }

        public event EventHandler AddressSelected;

        private void AddressSelectedHandler(object sender, EventArgs e)
        {
            if (AddressSelected != null)
                AddressSelected(this, EventArgs.Empty);
        }

        public event EventHandler PositionSelected;

        private void PositionSelectedHandler(object sender, EventArgs e)
        {
            if (PositionSelected != null)
                PositionSelected(this, EventArgs.Empty);
        }

        public event EventHandler SexSelected;

        private void SexSelectedHandler(object sender, EventArgs e)
        {
            if (SexSelected != null)
                SexSelected(this, EventArgs.Empty);
        }

        public event EventHandler PhotosOpened;

        private void PhotosOpenedHandler(object sender, EventArgs e)
        {
            if (PhotosOpened != null)
                PhotosOpened(this, EventArgs.Empty);
        }

        public event EventHandler PhotoCleared;

        private void PhotoClearedHandler(object sender, EventArgs e)
        {
            if (PhotoCleared != null)
                PhotoCleared(this, EventArgs.Empty);
        }

        public event EventHandler AddAddress;

        private void AddAddressHandler(object sender, EventArgs e)
        {
            if (AddAddress != null)
                AddAddress(this, EventArgs.Empty);
        }

        public event EventHandler AddPosition;

        private void AddPositionHandler(object sender, EventArgs e)
        {
            if (AddPosition != null)
                AddPosition(this, EventArgs.Empty);
        }

        private void textBoxSurname_TextChanged(object sender, EventArgs e)
        {
            buttonOk.Enabled = Surname.Length > 0 && NameOfEmployee.Length > 0 && Patronymic.Length > 0
                               && Phone.Length > 0;
        }

        private void textBoxPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '-'))
            {
                e.Handled = true;
            }
        }
    }
}