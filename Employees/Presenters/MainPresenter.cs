using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Employees.DomainModel;
using Employees.MVP;
using Employees.Utilities;
using Employees.Views;

namespace Employees.Presenters
{
    public class MainPresenter : BasePresenter<IMainView>
    {
        private IMyEmployeesDomainModel Model { get; set; }
        public string Role { get; set; }
        private string[] sex = { "All", "Male", "Female" };

        public MainPresenter(IMyEmployeesDomainModel domainModel, IMainView mainView)
        {
            // save references on model and view
            Model = domainModel;
            View = mainView;
            // subscribe to the event of view
            View.Loaded += OnLoaded;
            View.EditCities += OnEditCities;
            View.EditStreets += OnEditStreets;
            View.EditAddresses += OnEditAddresses;
            View.EditPositions += OnEditPositions;
            View.EditCertifications += OnEditCertifications;
            View.EditCertificationsOfEmployees += OnEditCertificationsOfEmployees;
            View.Add += OnAdd;
            View.Edit += OnEdit;
            View.Delete += OnDelete;
            View.OpenPhoto += OnOpenPhoto;
            View.SearchEmployees += OnSearchEmployees;
            View.ShowAllEmployees += OnShowAllEmployees;
            View.ShowCertificationsOfEmployee += OnShowCertificationsOfEmployee;
            View.ListViewSelectedIndexChanged += OnListView1SelectedIndexChanged;
            View.ShowSalary += OnShowSalary;
            View.OpenAboutForm += OnOpenAboutForm;
        }

        // Downloads data in Listview when the form is loaded
        private void OnLoaded(object sender, EventArgs e)
        {
            UpdateView();

            try
            {
                foreach (var s in sex)
                {
                    View.SexComboBox.Items.Add(s);
                }

                View.SexComboBox.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("There is an error by getting sexes.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Utils.Log.Trace(ex);
            }
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

        private void UpdateView()
        {
            try
            {
                List<City> cities = Model.CityRepository.GetAll().ToList();
                List<Street> streets = Model.StreetRepository.GetAll().ToList();
                List<Address> addresses = Model.AddressRepository.GetAll().ToList();
                List<Position> positions = Model.PositionRepository.GetAll().ToList();
                IEnumerable<Employee> employees = Model.EmployeeRepository.GetAll();

                View.ListView.Items.Clear();

                foreach (var employee in employees)
                {
                    ListViewItem item = View.ListView.Items.Add(new ListViewItem());
                    item.Text = employee.Surname;
                    item.SubItems.Add(employee.Name);
                    item.SubItems.Add(employee.Patronymic);
                    item.SubItems.Add(employee.Sex);
                    item.SubItems.Add(cities.Find(c => c.Id == addresses.Find(a => a.Id == employee.AddressFk).CityFk).Name);
                    item.SubItems.Add(streets.Find(s => s.Id == addresses.Find(a => a.Id == employee.AddressFk).StreetFk).Name);
                    item.SubItems.Add(addresses.Find(a => a.Id == employee.AddressFk).House);
                    item.SubItems.Add(employee.Phone);
                    item.SubItems.Add(employee.DateOfBirth.ToString("d", CultureInfo.CurrentCulture));
                    item.SubItems.Add(positions.Find(p => p.Id == employee.PositionFk).Name);
                    item.SubItems.Add(employee.Photo);

                    item.Tag = employee;
                }

                View.ListView.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("There is an error by getting employees.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Utils.Log.Trace(ex);
            }
        }

        private void OnAdd(object sender, EventArgs e)
        {
            if (Model.AddressRepository.GetAll().ToList().Count == 0)
            {
                MessageBox.Show("Handbook \"Addresses\" is empty. It must contain at least one address.",
                    "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (Model.CityRepository.GetAll().ToList().Count == 0)
            {
                MessageBox.Show("Handbook \"Cities\" is empty. It must contain at least one city.",
                    "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (Model.StreetRepository.GetAll().ToList().Count == 0)
            {
                MessageBox.Show("Handbook \"Streets\" is empty. It must contain at least one street.",
                    "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (Model.PositionRepository.GetAll().ToList().Count == 0)
            {
                MessageBox.Show("Handbook \"Positions\" is empty. It must contain at least one position.",
                    "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var employeeAddEditPresenter = new EmployeesAddEditPresenter(Model, new EmployeeItemForm(), "New")
            {
                Title = "New employee",
                GroupBoxText = "Information about new employee",
                Role = Role
            };

            int selectedIndex = 0;

            if (View.ListView.SelectedIndices.Count != 0)
                selectedIndex = View.ListView.SelectedIndices[0];

            if (((Form) employeeAddEditPresenter.View).ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var employee = new Employee()
                    {
                        Id = 0,
                        Surname = employeeAddEditPresenter.View.Surname,
                        Name = employeeAddEditPresenter.View.NameOfEmployee,
                        Patronymic = employeeAddEditPresenter.View.Patronymic,
                        Sex = employeeAddEditPresenter.Item == 0 ? "M" : "F",
                        Phone = employeeAddEditPresenter.View.Phone,
                        DateOfBirth = employeeAddEditPresenter.View.DateOfBirth,
                        AddressFk = employeeAddEditPresenter.AddressFk,
                        PositionFk = employeeAddEditPresenter.PositionFk,
                        Photo = employeeAddEditPresenter.FileName
                    };

                    Model.EmployeeRepository.Add(employee);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("There is an error by adding new employee.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Utils.Log.Trace(ex);
                }
            }

            ListView.ListViewItemCollection collection = View.ListView.Items;
            int count = collection.Count;

            // notify subscribers about what have changed
            ModifiedHandler();

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
            Employee employee = (Employee)View.ListView.Items[selectedIndex].Tag;

            var employeeAddEditPresenter = new EmployeesAddEditPresenter(Model, new EmployeeItemForm(), "");
            employeeAddEditPresenter.Title = "Existing employee changing";
            employeeAddEditPresenter.GroupBoxText = "Information about employee";

            employeeAddEditPresenter.View.Surname = employee.Surname;
            employeeAddEditPresenter.View.NameOfEmployee = employee.Name;
            employeeAddEditPresenter.View.Patronymic = employee.Patronymic;
            employeeAddEditPresenter.View.Phone = employee.Phone;
            employeeAddEditPresenter.View.DateOfBirth = employee.DateOfBirth;
            employeeAddEditPresenter.Item = employee.Sex == "M" ? 0 : 1;
            employeeAddEditPresenter.AddressFk = employee.AddressFk;
            employeeAddEditPresenter.PositionFk = employee.PositionFk;
            employeeAddEditPresenter.FileName = employee.Photo;
            employeeAddEditPresenter.Role = Role;
            
            if (((Form)employeeAddEditPresenter.View).ShowDialog() == DialogResult.OK)
            {
                try
                {
                    employee.Name = employeeAddEditPresenter.View.NameOfEmployee;
                    employee.Surname = employeeAddEditPresenter.View.Surname;
                    employee.Patronymic = employeeAddEditPresenter.View.Patronymic;
                    employee.Phone = employeeAddEditPresenter.View.Phone;
                    employee.DateOfBirth = employeeAddEditPresenter.View.DateOfBirth;
                    employee.Sex = employeeAddEditPresenter.Item == 0 ? "M" : "F";
                    employee.AddressFk = employeeAddEditPresenter.AddressFk;
                    employee.PositionFk = employeeAddEditPresenter.PositionFk;
                    employee.Photo = employeeAddEditPresenter.FileName;

                    Model.EmployeeRepository.Update(employee);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("There is an error by editing employee.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Utils.Log.Trace(ex);
                }
            }

            // notify subscribers about what have changed
            ModifiedHandler();

            UpdateView();

            // select element in, which was changing
            ListView.ListViewItemCollection collection = View.ListView.Items;
            View.ListView.Select();
            collection[selectedIndex].Selected = true;
            View.ListView.EnsureVisible(selectedIndex);
        }

        private void OnDelete(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection selectedIndices = View.ListView.SelectedIndices;

            if (selectedIndices.Count == 0 || MessageBox.Show("These employees will be removed.\n\n Are you sure you want to remove these employees?", "Employees removing",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                return;

            try
            {
                for (var i = selectedIndices.Count - 1; i >= 0; i--)
                {
                    Model.EmployeeRepository.Delete(((Employee)View.ListView.Items[selectedIndices[i]].Tag).Id);
                    View.ListView.Items.RemoveAt(selectedIndices[i]);
                }

                View.ListView.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("There is an error by removing an employee.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Utils.Log.Trace(ex);
            }

            // notify subscribers about what have changed
            ModifiedHandler();
        }

        // Cities
        private void OnEditCities(object sender, EventArgs e)
        {
            var cityPresenter = new CitiesPresenter(Model, new RefForm());
            cityPresenter.Role = Role;

            if (cityPresenter.Role == "Administrator")
            {
                cityPresenter.View.AddButton.Enabled = true;
            }

            cityPresenter.Modified += OnModified;
            ((Form)cityPresenter.View).ShowDialog();
            cityPresenter.Modified -= OnModified;
        }

        // Streets
        private void OnEditStreets(object sender, EventArgs e)
        {
            var streetPresenter = new StreetsPresenter(Model, new RefForm());
            streetPresenter.Role = Role;

            if (streetPresenter.Role == "Administrator")
            {
                streetPresenter.View.AddButton.Enabled = true;
            }

            streetPresenter.Modified += OnModified;
            ((Form)streetPresenter.View).ShowDialog();
            streetPresenter.Modified -= OnModified;
        }

        // Addresses
        private void OnEditAddresses(object sender, EventArgs e)
        {
            var addressPresenter = new AddressesPresenter(Model, new AddressForm());
            addressPresenter.Role = Role;

            if (addressPresenter.Role == "Administrator")
            {
                addressPresenter.View.AddButton.Enabled = true;
            }

            addressPresenter.Modified += OnModified;
            ((Form)addressPresenter.View).ShowDialog();
            addressPresenter.Modified -= OnModified;
        }

        // Positions
        private void OnEditPositions(object sender, EventArgs e)
        {
            var positionPresenter = new PositionsPresenter(Model, new PositionForm());
            positionPresenter.Role = Role;

            if (positionPresenter.Role == "Administrator")
            {
                positionPresenter.View.AddButton.Enabled = true;
            }

            positionPresenter.Modified += OnModified;
            ((Form)positionPresenter.View).ShowDialog();
            positionPresenter.Modified -= OnModified;
        }

        // Certifications
        private void OnEditCertifications(object sender, EventArgs e)
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
        }

        // Certifications of employees
        private void OnEditCertificationsOfEmployees(object sender, EventArgs e)
        {
            var certificationsOfEmployeesPresenter = new CertificationsOfEmployeesPresenter(Model, new CertificationsOfEmployeeForm());
            certificationsOfEmployeesPresenter.Role = Role;

            if (certificationsOfEmployeesPresenter.Role == "Administrator")
            {
                certificationsOfEmployeesPresenter.View.AddButton.Enabled = true;
            }

            certificationsOfEmployeesPresenter.Modified += OnModified;
            ((Form)certificationsOfEmployeesPresenter.View).ShowDialog();
            certificationsOfEmployeesPresenter.Modified -= OnModified;

            UpdateView();
        }

        private void OnOpenPhoto(object sender, EventArgs e)
        {
            List<Position> positions = Model.PositionRepository.GetAll().ToList();
            string fileName = View.ListView.SelectedItems[0].SubItems[10].Text;
            string pathForPhoto = Application.StartupPath + @"\Photos" + @"\" + fileName;
            Image image = Image.FromFile(pathForPhoto);
            Employee employee = (Employee)View.ListView.SelectedItems[0].Tag;

            var imageForm = new ImageForm();
            imageForm.Picture.Image = image;
            imageForm.Picture.SizeMode = PictureBoxSizeMode.StretchImage;
            imageForm.EmployeeLabel.Text = employee.Surname + " " + employee.Name + " " + employee.Patronymic + " - " +
                positions.Find(p => p.Id == employee.PositionFk).Name;

            var selectedIndex = View.ListView.SelectedIndices[0];

            imageForm.ShowDialog();

            // select element of Listview, which photo we looked
            ListView.ListViewItemCollection collection = View.ListView.Items;
            View.ListView.Select();
            for (int i = View.ListView.SelectedIndices.Count - 1; i >= 0 ; i--)
            {
                View.ListView.SelectedItems[i].Selected = false;
            }
            collection[selectedIndex].Selected = true;
            View.ListView.EnsureVisible(selectedIndex);
        }

        private void OnSearchEmployees(object sender, EventArgs e)
        {
            try
            {
                List<City> cities = Model.CityRepository.GetAll().ToList();
                List<Street> streets = Model.StreetRepository.GetAll().ToList();
                List<Address> addresses = Model.AddressRepository.GetAll().ToList();
                List<Position> positions = Model.PositionRepository.GetAll().ToList();
                IEnumerable<Employee> employees = Model.EmployeeRepository.GetAll();
                
                var employeesSurname = new List<Employee>();
                var employeesName = new List<Employee>();
                var employeesPatronymic = new List<Employee>();
                var employeesPosition = new List<Employee>();
                var employeesSex = new List<Employee>();

                View.ListView.Items.Clear();

                // At first it checks surname: if it matches, it will add this employee into another array.
                // If there are no elements in array, next fields will not be checked.
                // Each search criterion works in such way.
                // P.S. If any criterion is empty, this criterion won't be checked,
                // so all employees will be added into the new array
                
                // Surnames
                if (View.SurnameTextBox.Length == 0)
                {
                    foreach (var employee in employees)
                    {
                        employeesSurname.Add(employee);
                    }
                }
                else
                {
                    employeesSurname.AddRange(employees.Where(employee => View.SurnameTextBox.ToUpper() == employee.Surname.ToUpper()));
                }

                // Names
                if (employeesSurname.Count != 0)
                {
                    if (View.NameTextBox.Length == 0)
                    {
                        // LINQ
                        employeesName.AddRange(employeesSurname);
                    }
                    else
                    {
                        // LINQ // second variant
                        employeesName.AddRange(employeesSurname.Where(employee => String.Equals(View.NameTextBox, employee.Name, StringComparison.CurrentCultureIgnoreCase)));    
                    }
                }

                // Patronymics
                if (employeesName.Count != 0)
                {
                    // second variant
                    employeesPatronymic.AddRange(View.PatronymicTextBox.Length == 0
                        ? employeesName : employeesName.Where(employee => String.Equals(View.PatronymicTextBox, employee.Patronymic, StringComparison.CurrentCultureIgnoreCase)));
                }

                // Positions
                if (employeesPatronymic.Count != 0)
                {
                    // second variant
                    employeesPosition.AddRange(View.PositionTextBox.Length == 0
                        ? employeesPatronymic : employeesPatronymic.Where(employee => View.PositionTextBox.ToUpper() == positions.Find(p => p.Id == employee.PositionFk).Name.ToUpper()));
                }

                // Sexes
                if (employeesPosition.Count != 0)
                {
                    string sexs = View.SexComboBox.SelectedIndex == 1 ? "M" : View.SexComboBox.SelectedIndex == 2 ? "F" : "";

                    employeesSex.AddRange(View.SexComboBox.SelectedIndex == 0 ? employeesPosition
                        : employeesPosition.Where(employee => sexs == employee.Sex));
                }

                if (employeesSex.Count > 0)
                {
                    foreach (var employee in employeesSex)
                    {
                        ListViewItem item = View.ListView.Items.Add(new ListViewItem());
                        item.Text = employee.Surname;
                        item.SubItems.Add(employee.Name);
                        item.SubItems.Add(employee.Patronymic);
                        item.SubItems.Add(employee.Sex);
                        item.SubItems.Add(cities.Find(c => c.Id == addresses.Find(a => a.Id == employee.AddressFk).CityFk).Name);
                        item.SubItems.Add(streets.Find(s => s.Id == addresses.Find(a => a.Id == employee.AddressFk).StreetFk).Name);
                        item.SubItems.Add(addresses.Find(a => a.Id == employee.AddressFk).House);
                        item.SubItems.Add(employee.Phone);
                        item.SubItems.Add(employee.DateOfBirth.ToString("d", CultureInfo.CurrentCulture));
                        item.SubItems.Add(positions.Find(p => p.Id == employee.PositionFk).Name);
                        item.SubItems.Add(employee.Photo);

                        item.Tag = employee;
                    }
                }
                else
                {
                    MessageBox.Show("Employees are not found", "Search is finished");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There is an error by getting employees.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Utils.Log.Trace(ex);
            }
        }

        private void OnShowAllEmployees(object sender, EventArgs e)
        {
            UpdateView();

            View.SurnameTextBox = "";
            View.NameTextBox = "";
            View.PatronymicTextBox = "";
            View.PositionTextBox = "";
            View.SexComboBox.SelectedIndex = 0;
        }

        private void OnShowCertificationsOfEmployee(object sender, EventArgs e)
        {
            List<CertificationsOfEmployee> certificationsOfEmployees = Model.CertificationOfEmployeeRepository.GetAll().ToList();
            List<Position> positions = Model.PositionRepository.GetAll().ToList();
            List<Certification> certifications = Model.CertificationRepository.GetAll().ToList();
            List<CertificationsOfEmployee> certificationsOfEmployee = new List<CertificationsOfEmployee>();

            Employee employee = (Employee)View.ListView.SelectedItems[0].Tag;
            
            var listOfCertificationsForm = new ListOfCertificationsForm();
            listOfCertificationsForm.EmployeeLabel.Text = employee.Surname + " " + employee.Name + " " + employee.Patronymic + " - " +
                positions.Find(p => p.Id == employee.PositionFk).Name;

            certificationsOfEmployee.AddRange(certificationsOfEmployees.Where(cert => cert.EmployeeFk == employee.Id));

            if (certificationsOfEmployee.Count == 0)
                listOfCertificationsForm.CertificationsListBox.Items.Add("This employee doesn't have certifications");
            else
            {
                foreach (var cert in certificationsOfEmployee)
                {
                    listOfCertificationsForm.CertificationsListBox.Items.Add(certifications.Find(c => c.Id == cert.CertificationFk).Name);
                }    
            }

            var selectedIndex = View.ListView.SelectedIndices[0];
            
            listOfCertificationsForm.ShowDialog();

            // select element in ListView, which certifications we looked
            ListView.ListViewItemCollection collection = View.ListView.Items;
            View.ListView.Select();
            for (int i = View.ListView.SelectedIndices.Count - 1; i >= 0; i--)
            {
                View.ListView.SelectedItems[i].Selected = false;
            }
            collection[selectedIndex].Selected = true;
            View.ListView.EnsureVisible(selectedIndex);
        }

        private void OnListView1SelectedIndexChanged(object sender, EventArgs e)
        {
            View.ButtonOpenPhoto.Enabled = View.ListView.SelectedItems.Count != 0;
            View.ButtonShowCertifications.Enabled = View.ListView.SelectedItems.Count != 0;

            if (Role != "Administrator")
                return;

            View.EditButton.Enabled = View.ListView.SelectedItems.Count != 0;
            View.DeleteButton.Enabled = View.ListView.SelectedItems.Count != 0;
        }

        private void OnShowSalary(object sender, EventArgs e)
        {
            var salaryPresenter = new SalaryPresenter(Model, new SalaryForm());
            ((Form)salaryPresenter.View).ShowDialog();
        }

        private void OnOpenAboutForm(object sender, EventArgs e)
        {
            var aboutForm = new AboutForm();
            aboutForm.ShowDialog();
        }
    }
}