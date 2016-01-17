using System.Windows.Forms;

namespace Employees.Views
{
    public partial class ListOfCertificationsForm : Form
    {
        public ListOfCertificationsForm()
        {
            InitializeComponent();
        }

        public ListBox CertificationsListBox
        {
            get { return listBoxCertifications; }
        }

        public Label EmployeeLabel
        {
            get { return labelEmployee; } 
        }
    }
}