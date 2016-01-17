using System.Windows.Forms;

namespace Employees.Views
{
    public partial class ImageForm : Form
    {
        public ImageForm()
        {
            InitializeComponent();
        }

        public PictureBox Picture
        {
            get { return pictureBox1; }
        }

        public Label EmployeeLabel
        {
            get { return labelEmployee; }
        }
    }
}