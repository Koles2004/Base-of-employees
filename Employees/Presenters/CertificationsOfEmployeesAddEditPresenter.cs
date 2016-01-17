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
    public class CertificationsOfEmployeesAddEditPresenter : BasePresenter<ICertificationsOfEmployeeAddEditView>
    {
        private IMyEmployeesDomainModel Model { get; set; }
        private List<Employee> Employees { get; set; }
        private List<Certification> Certifications { get; set; }
        private List<Position> Positions { get; set; }
        public string Role { get; set; }

        public string Title { set { ((Form)View).Text = value; } }
        public long EmployeeFk { get; set; }
        public long CertificationFk { get; set; }

        public CertificationsOfEmployeesAddEditPresenter(IMyEmployeesDomainModel domainModel, ICertificationsOfEmployeeAddEditView certificationsOfEmployeeAddEditView)
        {
            Model = domainModel;
            View = certificationsOfEmployeeAddEditView;
            View.Loaded += OnLoaded;
            View.EmployeeSelected += OnEmployeeSelected;
            View.CertificationSelected += OnCertificationSelected;
            View.AddCertification += OnAddCertifications;
            View.AddEmployee += OnAddEmployees;
        }

        private void OnEmployeeSelected(object sender, EventArgs e)
        {
            // When the user selects from the drop-down list another employee, remember its Id
            EmployeeFk = Employees[View.EmployeeComboBox.SelectedIndex].Id;
        }

        private void OnCertificationSelected(object sender, EventArgs e)
        {
            // When the user selects from the drop-down list another certification, remember its Id
            CertificationFk = Certifications[View.CertificationComboBox.SelectedIndex].Id;
        }

        private void OnModified(object sender, EventArgs e)
        {
            UpdateView();
        }

        public event EventHandler Modified;

        private void UpdateView()
        {
            View.CertificationComboBox.Items.Clear();
            View.EmployeeComboBox.Items.Clear();

            try
            {
                Employees = Model.EmployeeRepository.GetAll().ToList();
                Positions = Model.PositionRepository.GetAll().ToList();

                foreach (var employee in Employees)
                    View.EmployeeComboBox.Items.Add(employee.Surname + " " + employee.Name + " " + employee.Patronymic + " " +
                        Positions.Find(p => p.Id == employee.PositionFk).Name);

                View.EmployeeComboBox.SelectedIndex = EmployeeFk == 0 ? 0 : Employees.FindIndex(c => c.Id == EmployeeFk); // there is an employee in ComboBox when form is loaded

                Certifications = Model.CertificationRepository.GetAll().ToList();
                foreach (var certification in Certifications)
                    View.CertificationComboBox.Items.Add(certification.Name);

                View.CertificationComboBox.SelectedIndex = CertificationFk == 0 ? 0 : Certifications.FindIndex(c => c.Id == CertificationFk); // there is a certification in ComboBox when form is loaded
            }
            catch (Exception ex)
            {
                MessageBox.Show("There is an error by getting cities and/or streets.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Utils.Log.Trace(ex);
            }
        }

        private void OnLoaded(object sender, EventArgs e)
        {
            UpdateView();
        }

        private void OnAddCertifications(object sender, EventArgs e)
        {
            var certificationPresenter = new CertificationsPresenter(Model, new RefForm());
            certificationPresenter.Role = Role;

            if (certificationPresenter.Role == "Administrator")
            {
                certificationPresenter.View.AddButton.Enabled = true;
            }

            certificationPresenter.Modified += OnModified;
            ((Form)certificationPresenter.View).ShowDialog();
            certificationPresenter.Modified -= OnModified;

            UpdateView();
        }

        private void OnAddEmployees(object sender, EventArgs e)
        {
            var mainPresenter = new MainPresenter(Model, new MainForm());
            mainPresenter.Role = Role;

            if (mainPresenter.Role == "Administrator")
            {
                mainPresenter.View.AddButton.Enabled = true;
            }

            mainPresenter.Modified += OnModified;
            ((Form)mainPresenter.View).ShowDialog();
            mainPresenter.Modified -= OnModified;

            UpdateView();
        }
    }
}