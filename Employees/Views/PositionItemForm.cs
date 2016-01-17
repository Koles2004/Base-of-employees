using System.Windows.Forms;

namespace Employees.Views
{
    public partial class PositionItemForm : Form
    {
        public PositionItemForm()
        {
            InitializeComponent();
        }

        public string GroupBoxText { set { groupBox1.Text = value; } }

        public string TextBoxPosition
        {
            get { return textBoxPosition.Text.Trim(); }
            set { textBoxPosition.Text = value; }
        }

        public string TextBoxSalary
        {
            get { return textBoxSalary.Text.Trim(); }
            set { textBoxSalary.Text = value; }
        }

        private void textBoxPosition_TextChanged(object sender, System.EventArgs e)
        {
            buttonOk.Enabled = TextBoxPosition.Length > 0 && TextBoxSalary.Length > 0;
        }

        private void textBoxSalary_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}