using System;
using System.Windows.Forms;
using Employees.DomainModelEntity;
using Employees.Presenters;

namespace Employees.Views
{
    public partial class StartForm : Form
    {
        public StartForm()
        {
            InitializeComponent();
            comboBoxChooseUser.Items.Add("Administrator");
            comboBoxChooseUser.Items.Add("Director");
        }

        public string ComboBoxChoose
        {
            get { return comboBoxChooseUser.SelectedItem.ToString(); }
        }

        public int ComboBoxIndex
        {
            get { return comboBoxChooseUser.SelectedIndex; }
        }

        public string TextBoxPassword
        {
            get { return textBoxPassword.Text; }
        }

        private void textBoxPassword_TextChanged(object sender, EventArgs e)
        {
            if (TextBoxPassword.Length > 0 && ComboBoxIndex != -1)
            {
                buttonOk.Enabled = true;
            }
            else
            {
                buttonOk.Enabled = false;
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (ComboBoxChoose == TextBoxPassword)
            {
                using(EmployeesDomainModel employeesDomainModel = new EmployeesDomainModel())
                {
                    var mainPresenter = new MainPresenter(employeesDomainModel, new MainForm());
                    mainPresenter.Role = ComboBoxChoose;

                    if (mainPresenter.Role == "Administrator")
                    {
                        mainPresenter.View.AddButton.Enabled = true;
                    }

                    ((Form)mainPresenter.View).ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Wrong password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }

        private void StartForm_Load(object sender, EventArgs e)
        {
            comboBoxChooseUser.SelectedIndex = 0;
        }
    }
}