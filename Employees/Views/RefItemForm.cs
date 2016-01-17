using System;
using System.Windows.Forms;

namespace Employees.Views
{
    public partial class RefItemForm : Form
    {
        public RefItemForm()
        {
            InitializeComponent();
        }

        public string GroupBoxText { set { groupBox1.Text = value; } }
        public string LabelText { set { label1.Text = value; } }

        public string TextBoxText
        {
            get { return textBox1.Text.Trim(); }
            set { textBox1.Text = value; }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            buttonOk.Enabled = TextBoxText.Length > 0;
        }
    }
}