using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Employees.DomainModel;
using Employees.MVP;
using Employees.Utilities;
using Employees.Views;

namespace Employees.Presenters
{
    public class CertificationsOfEmployeesPresenter : BasePresenter<ICertificationsOfEmployeeView>
    {
        private IMyEmployeesDomainModel Model { get; set; }
        public string Role { get; set; }

        public CertificationsOfEmployeesPresenter(IMyEmployeesDomainModel domainModel, ICertificationsOfEmployeeView certificationsOfEmployeeView)
        {
            // save references on model and view
            Model = domainModel;
            View = certificationsOfEmployeeView;
            // subscribe to the event of view
            View.Loaded += OnLoaded;
            View.Add += OnAdd;
            View.Edit += OnEdit;
            View.Delete += OnDelete;
            View.ListViewSelectedIndexChanged += OnListView1SelectedIndexChanged;
        }

        // Clear the list of certificationsOfEmployees in the view and fill it again from the model
        private void UpdateView()
        {
            try
            {
                List<Employee> employees = Model.EmployeeRepository.GetAll().ToList();
                List<Certification> certifications = Model.CertificationRepository.GetAll().ToList();
                List<Position> positions = Model.PositionRepository.GetAll().ToList();
                IEnumerable<CertificationsOfEmployee> certificationsOfEmployees = Model.CertificationOfEmployeeRepository.GetAll();

                View.ListView.Items.Clear();

                foreach (var certificationsOfEmployee in certificationsOfEmployees)
                {
                    ListViewItem item = View.ListView.Items.Add(new ListViewItem());
                    item.Text = employees.Find(e => e.Id == certificationsOfEmployee.EmployeeFk).Surname;
                    item.SubItems.Add(employees.Find(e => e.Id == certificationsOfEmployee.EmployeeFk).Name);
                    item.SubItems.Add(employees.Find(e => e.Id == certificationsOfEmployee.EmployeeFk).Patronymic);
                    item.SubItems.Add(positions.Find(p => p.Id == employees.Find(e => e.Id == certificationsOfEmployee.EmployeeFk).PositionFk).Name);
                    item.SubItems.Add(certifications.Find(c => c.Id == certificationsOfEmployee.CertificationFk).Name);

                    item.Tag = certificationsOfEmployee;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There is an error by getting certificationsOfEmployees.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Utils.Log.Trace(ex);
            }

            View.ListView.Focus();
        }

        private void OnLoaded(object sender, EventArgs e)
        {
            UpdateView();
        }

        private void OnModified(object sender, EventArgs e)
        {
            UpdateView();
        }

        public event EventHandler Modified;

        private void ModifiedHandler()
        {
            if (Modified != null)
                Modified(this, EventArgs.Empty);
        }

        private void OnAdd(object sender, EventArgs e)
        {
            if (Model.CertificationRepository.GetAll().ToList().Count == 0)
            {
                MessageBox.Show("Handbook \"Certifications\" is empty. It must contain at least one certification.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (Model.EmployeeRepository.GetAll().ToList().Count == 0)
            {
                MessageBox.Show("Handbook \"Employees\" is empty. It must contain at least one employee.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var certificationOfEmployeesAddEditPresenter = new CertificationsOfEmployeesAddEditPresenter(Model, new CertificationsOfEmployeeItemForm());
            certificationOfEmployeesAddEditPresenter.Title = "New certification of employee";
            certificationOfEmployeesAddEditPresenter.Role = Role;

            int selectedIndex = 0;

            if (View.ListView.SelectedIndices.Count != 0)
                selectedIndex = View.ListView.SelectedIndices[0];

            if (((Form)certificationOfEmployeesAddEditPresenter.View).ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Model.CertificationOfEmployeeRepository.Add(new CertificationsOfEmployee()
                    {
                        Id = 0,
                        EmployeeFk = certificationOfEmployeesAddEditPresenter.EmployeeFk,
                        CertificationFk = certificationOfEmployeesAddEditPresenter.CertificationFk
                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show("There is an error by adding new certificationOfEmployee.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Utils.Log.Trace(ex);
                }
            }

            ListView.ListViewItemCollection collection = View.ListView.Items;
            int count = collection.Count;

            UpdateView();

            // select added element in Listview
            if (count < collection.Count)
            {
                View.ListView.Select();
                collection[collection.Count - 1].Selected = true;
                View.ListView.EnsureVisible(collection.Count - 1);
            }
            else
            {
                if (selectedIndex == 0)
                    return;
                View.ListView.Select();
                collection[selectedIndex].Selected = true;
                View.ListView.EnsureVisible(selectedIndex);
            }
        }

        private void OnEdit(object sender, EventArgs e)
        {
            // for MouseDoubleClick on ListView
            if (Role != "Administrator")
                return;

            // Check whether there is at least one selected item in the list
            if (View.ListView.SelectedItems.Count == 0)
                return;

            // If there are several selected items, take the first
            int selectedIndex = View.ListView.SelectedIndices[0];
            CertificationsOfEmployee certificationOfEmployeee = (CertificationsOfEmployee)View.ListView.Items[selectedIndex].Tag;

            var certificationOfEmployeeAddEditPresenter  = new CertificationsOfEmployeesAddEditPresenter(Model, new CertificationsOfEmployeeItemForm());
            certificationOfEmployeeAddEditPresenter.Title = "Existing certificationOfEmployee changing";
            certificationOfEmployeeAddEditPresenter.EmployeeFk = certificationOfEmployeee.EmployeeFk;
            certificationOfEmployeeAddEditPresenter.CertificationFk = certificationOfEmployeee.CertificationFk;
            certificationOfEmployeeAddEditPresenter.Role = Role;

            if (((Form)certificationOfEmployeeAddEditPresenter.View).ShowDialog() == DialogResult.OK)
            {
                try
                {
                    certificationOfEmployeee.EmployeeFk = certificationOfEmployeeAddEditPresenter.EmployeeFk;
                    certificationOfEmployeee.CertificationFk = certificationOfEmployeeAddEditPresenter.CertificationFk;
                    
                    Model.CertificationOfEmployeeRepository.Update(certificationOfEmployeee);

                    // notify subscribers about what have changed
                    ModifiedHandler();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("There is an error by editing certificationOfEmployee.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Utils.Log.Trace(ex);
                }
            }

            UpdateView();

            // select the element in ListView, which was changing
            ListView.ListViewItemCollection collection = View.ListView.Items;
            View.ListView.Select();
            collection[selectedIndex].Selected = true;
            View.ListView.EnsureVisible(selectedIndex);
        }

        private void OnDelete(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection selectedIndices = View.ListView.SelectedIndices;

            if (selectedIndices.Count == 0 || MessageBox.Show("Certifications of these employees will be removed.\n\nAre you sure you want to remove them?", "Certifications of employees removing",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                return;

            try
            {
                for (int i = selectedIndices.Count - 1; i >= 0; i--)
                {
                    Model.CertificationOfEmployeeRepository.Delete(((CertificationsOfEmployee)View.ListView.Items[selectedIndices[i]].Tag).Id);
                    View.ListView.Items.RemoveAt(selectedIndices[i]);
                }

                View.ListView.Focus();

                // notify subscribers about what have changed
                ModifiedHandler();
            }
            catch (Exception ex)
            {
                MessageBox.Show("There is an error by removing certificationsOfEmployees.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Utils.Log.Trace(ex);
            }
        }

        private void OnListView1SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Role != "Administrator")
                return;

            View.EditButton.Enabled = View.ListView.SelectedItems.Count != 0;
            View.DeleteButton.Enabled = View.ListView.SelectedItems.Count != 0;
        }
    }
}