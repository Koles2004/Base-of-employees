using System;
using System.Windows.Forms;

namespace Employees.Views
{
    public partial class CertificationsOfEmployeeItemForm : Form, ICertificationsOfEmployeeAddEditView
    {
        public CertificationsOfEmployeeItemForm()
        {
            InitializeComponent();
        }

        public ComboBox EmployeeComboBox { get { return comboBoxEmployee; } }
        public ComboBox CertificationComboBox { get { return comboBoxCertification; } }

        public event EventHandler Loaded;

        private void LoadedHandler(object sender, EventArgs e)
        {
            if (Loaded != null)
                Loaded(this, EventArgs.Empty);
        }

        public event EventHandler EmployeeSelected;

        private void EmployeeSelectedHandler(object sender, EventArgs e)
        {
            if (EmployeeSelected != null)
                EmployeeSelected(this, EventArgs.Empty);
        }

        public event EventHandler CertificationSelected;

        private void CertificationSelectedHandler(object sender, EventArgs e)
        {
            if (CertificationSelected != null)
                CertificationSelected(this, EventArgs.Empty);
        }

        public event EventHandler AddCertification;

        private void AddCertificationHandler(object sender, EventArgs e)
        {
            if (AddCertification != null)
                AddCertification(this, EventArgs.Empty);
        }

        public event EventHandler AddEmployee;

        private void AddEmployeeHandler(object sender, EventArgs e)
        {
            if (AddEmployee != null)
                AddEmployee(this, EventArgs.Empty);
        }
    }
}