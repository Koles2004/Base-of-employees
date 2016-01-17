using System;
using System.Windows.Forms;

namespace Employees.Views
{
    public partial class CertificationsOfEmployeeForm : Form, ICertificationsOfEmployeeView
    {
        public CertificationsOfEmployeeForm()
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

        public ListView ListView { get { return listView1; } }

        public event EventHandler Loaded;

        private void LoadedHandler(object sender, EventArgs e)
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

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            EditHandler(sender, e);
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        public event EventHandler ListViewSelectedIndexChanged;

        private void ListViewSelectedIndexChangedHandler(object sender, EventArgs e)
        {
            if (ListViewSelectedIndexChanged != null)
                ListViewSelectedIndexChanged(this, EventArgs.Empty);
        }
    }
}