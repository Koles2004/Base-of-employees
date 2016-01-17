using System;
using System.Windows.Forms;

namespace Employees.Views
{
    public partial class SalaryForm : Form, ISalaryView
    {
        public SalaryForm()
        {
            InitializeComponent();
        }

        public ComboBox PositionsComboBox { get { return comboBoxPosition; } }

        public string SalaryLabel
        {
            get { return labelSalary.Text; }
            set { labelSalary.Text = value; }
        }

        public event EventHandler Loaded;

        private void LoadedHandler(object sender, EventArgs e)
        {
            if (Loaded != null)
                Loaded(this, EventArgs.Empty);
        }

        public event EventHandler PositionSelected;

        private void PositionSelectedHandler(object sender, EventArgs e)
        {
            if (PositionSelected != null)
                PositionSelected(this, EventArgs.Empty);
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}