using System;
using System.Windows.Forms;

namespace Employees.Views
{
    public partial class MainForm : Form, IMainView
    {
        public MainForm()
        {
            InitializeComponent();
        }

        public Button AddButton
        {
            get { return buttonAdd; }
            set { buttonAdd = value; }
        }

        public Button EditButton
        {
            get { return buttonEdit; }
            set { buttonEdit = value; }
        }

        public Button DeleteButton
        {
            get { return buttonDelete; }
            set { buttonDelete = value; }
        }

        public Button ButtonOpenPhoto { get { return buttonOpenPhoto; } }
        public Button ButtonShowCertifications { get { return buttonShowCertifications; } }
        
        public String SurnameTextBox
        {
            get { return textBoxSurname.Text.Trim(); }
            set { textBoxSurname.Text = value; }
        }

        public String NameTextBox
        {
            get { return textBoxName.Text.Trim(); }
            set { textBoxName.Text = value; }
        }

        public String PatronymicTextBox
        {
            get { return textBoxPatronymic.Text.Trim(); }
            set { textBoxPatronymic.Text = value; }
        }

        public String PositionTextBox
        {
            get { return textBoxPosition.Text.Trim(); }
            set { textBoxPosition.Text = value; }
        }

        public ComboBox SexComboBox { get { return comboBoxSex; } }

        public ListView ListView { get { return listView1; } }

        public event EventHandler Loaded;

        private void LoadHandler(object sender, EventArgs e)
        {
            if (Loaded != null)
                Loaded(this, EventArgs.Empty);
        }

        public event EventHandler Add;

        private void AddHandler(object sender, EventArgs e)
        {
            if (Add != null)
                Add(this, EventArgs.Empty);
        }

        public event EventHandler Edit;

        private void EditHandler(object sender, EventArgs e)
        {
            if (Edit != null)
                Edit(this, EventArgs.Empty);
        }

        public event EventHandler Delete;

        private void DeleteHandler(object sender, EventArgs e)
        {
            if (Delete != null)
                Delete(this, EventArgs.Empty);
        }

        public event EventHandler EditCities;

        private void EditCitiesHandler(object sender, EventArgs e)
        {
            if (EditCities != null)
                EditCities(this, EventArgs.Empty);
        }

        public event EventHandler EditStreets;

        private void EditStreetsHandler(object sender, EventArgs e)
        {
            if (EditStreets != null)
                EditStreets(this, EventArgs.Empty);
        }
        public event EventHandler EditAddresses;

        private void EditAddressesHandler(object sender, EventArgs e)
        {
            if (EditAddresses != null)
                EditAddresses(this, EventArgs.Empty);
        }
        public event EventHandler EditPositions;

        private void EditPositionsHandler(object sender, EventArgs e)
        {
            if (EditPositions != null)
                EditPositions(this, EventArgs.Empty);
        }

        public event EventHandler EditCertifications;

        private void EditCertificationsHandler(object sender, EventArgs e)
        {
            if (EditCertifications != null)
                EditCertifications(this, EventArgs.Empty);
        }

        public event EventHandler EditCertificationsOfEmployees;

        private void EditCertificationsOfEmployeesHandler(object sender, EventArgs e)
        {
            if (EditCertificationsOfEmployees != null)
                EditCertificationsOfEmployees(this, EventArgs.Empty);
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            EditHandler(sender, e);
        }

        public event EventHandler ShowSalary;

        private void ShowSalaryHandler(object sender, EventArgs e)
        {
            if (ShowSalary != null)
                ShowSalary(this, EventArgs.Empty);
        }

        public event EventHandler OpenPhoto;

        private void OpenPhotoHandler(object sender, EventArgs e)
        {
            if (OpenPhoto != null)
                OpenPhoto(this, EventArgs.Empty);
        }

        public event EventHandler OpenAboutForm;

        private void OpenAboutFormHandler(object sender, EventArgs e)
        {
            if (OpenAboutForm != null)
                OpenAboutForm(this, EventArgs.Empty);
        }

        public event EventHandler SearchEmployees;

        private void SearchEmployeesHandler(object sender, EventArgs e)
        {
            if (SearchEmployees != null)
                SearchEmployees(this, EventArgs.Empty);
        }

        public event EventHandler ShowAllEmployees;

        private void ShowAllEmployeesHandler(object sender, EventArgs e)
        {
            if (ShowAllEmployees != null)
                ShowAllEmployees(this, EventArgs.Empty);
        }

        public event EventHandler ShowCertificationsOfEmployee;

        private void ShowCertificationsOfEmployeeHandler(object sender, EventArgs e)
        {
            if (ShowCertificationsOfEmployee != null)
                ShowCertificationsOfEmployee(this, EventArgs.Empty);
        }

        public event EventHandler ListViewSelectedIndexChanged;

        private void ListViewSelectedIndexChangedHandler(object sender, EventArgs e)
        {
            if (ListViewSelectedIndexChanged != null)
                ListViewSelectedIndexChanged(this, EventArgs.Empty);
        }

        private void exitToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
                Close();
        }
    }
}