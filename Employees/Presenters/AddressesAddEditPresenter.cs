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
    public class AddressesAddEditPresenter: BasePresenter<IAddressAddEditView>
    {
        private IMyEmployeesDomainModel Model { get; set; }
        private List<City> Cities { get; set; }
        private List<Street> Streets { get; set; }

        public string Title { set { ((Form)View).Text = value; } }
        public long CityFk { get; set; }
        public long StreetFk { get; set; }
        public string Role { get; set; }

        public AddressesAddEditPresenter(IMyEmployeesDomainModel domainModel, IAddressAddEditView addressAddEditView)
        {
            Model = domainModel;
            View = addressAddEditView;
            View.Loaded += OnLoaded;
            View.CitySelected += OnCitySelected;
            View.StreetSelected += OnStreetSelected;
            View.AddCity += OnAddCity;
            View.AddStreet += OnAddStreet;
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

        private void OnCitySelected(object sender, EventArgs e)
        {
            // When the user selects from the drop-down list another city, remember its Id
            CityFk = Cities[View.CityComboBox.SelectedIndex].Id;
        }

        private void OnStreetSelected(object sender, EventArgs e)
        {
            // When the user selects from the drop-down list another street, remember its Id
            StreetFk = Streets[View.StreetComboBox.SelectedIndex].Id;
        }

        private void UpdateView()
        {
            View.CityComboBox.Items.Clear();
            View.StreetComboBox.Items.Clear();

            try
            {
                Cities = Model.CityRepository.GetAll().ToList();
                foreach (var city in Cities)
                    View.CityComboBox.Items.Add(city.Name);

                View.CityComboBox.SelectedIndex = CityFk == 0 ? 0 : Cities.FindIndex(c => c.Id == CityFk); // there is a city in ComboBox when form is loaded

                Streets = Model.StreetRepository.GetAll().ToList();
                foreach (var street in Streets)
                    View.StreetComboBox.Items.Add(street.Name);

                View.StreetComboBox.SelectedIndex = StreetFk == 0 ? 0 : Streets.FindIndex(c => c.Id == StreetFk); // there is a street in ComboBox when form is loaded
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

        private void OnAddCity(object sender, EventArgs e)
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

            UpdateView();
        }

        private void OnAddStreet(object sender, EventArgs e)
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

            UpdateView();
        }
    }
}