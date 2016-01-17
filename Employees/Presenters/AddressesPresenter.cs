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
    public class AddressesPresenter : BasePresenter<IAddressView>
    {
        private IMyEmployeesDomainModel Model { get; set; }
        public string Role { get; set; }

        public AddressesPresenter(IMyEmployeesDomainModel domainModel, IAddressView addressView)
        {
            // save references on model and view
            Model = domainModel;
            View = addressView;
            // subscribe to the event of view
            View.Loaded += OnLoaded;
            View.Add += OnAdd;
            View.Edit += OnEdit;
            View.Delete += OnDelete;
            View.ListViewSelectedIndexChanged += OnListView1SelectedIndexChanged;
        }

        // Clear the list of addresses in the view and fill it again from the model
        private void UpdateView()
        {
            try
            {
                List<City> cities = Model.CityRepository.GetAll().ToList();
                List<Street> streets = Model.StreetRepository.GetAll().ToList();
                IEnumerable<Address> addresses = Model.AddressRepository.GetAll();

                View.ListView.Items.Clear();

                foreach (var address in addresses)
                {
                    ListViewItem item = View.ListView.Items.Add(new ListViewItem());
                    item.Text = cities.Find(c => c.Id == address.CityFk).Name;
                    item.SubItems.Add(streets.Find(s => s.Id == address.StreetFk).Name);
                    item.SubItems.Add(address.House);
                    item.Tag = address;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There is an error by getting addresses.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (Model.CityRepository.GetAll().ToList().Count == 0)
            {
                MessageBox.Show("Handbook \"Cities\" is empty. It must contain at least one city.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (Model.StreetRepository.GetAll().ToList().Count == 0)
            {
                MessageBox.Show("Handbook \"Streets\" is empty. It must contain at least one street.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var addressAddEditPresenter = new AddressesAddEditPresenter(Model, new AddressItemForm())
            {
                Title = "New address",
                Role = Role
            };

            int selectedIndex = 0;

            if (View.ListView.SelectedIndices.Count != 0)
                selectedIndex = View.ListView.SelectedIndices[0];

            if (((Form)addressAddEditPresenter.View).ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Model.AddressRepository.Add(new Address()
                    {
                        Id = 0,
                        CityFk = addressAddEditPresenter.CityFk,
                        StreetFk = addressAddEditPresenter.StreetFk,
                        House = addressAddEditPresenter.View.House
                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show("There is an error by adding new address.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Utils.Log.Trace(ex);
                }
            }

            ListView.ListViewItemCollection collection = View.ListView.Items;
            int count = collection.Count;

            // notify subscribers about what have changed
            ModifiedHandler();

            UpdateView();

            // select added element in ListView
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
            Address address = (Address)View.ListView.Items[selectedIndex].Tag;

            var addressAddEditPresenter = new AddressesAddEditPresenter(Model, new AddressItemForm());
            addressAddEditPresenter.Title = "Existing address changing";
            addressAddEditPresenter.CityFk = address.CityFk;
            addressAddEditPresenter.StreetFk = address.StreetFk;
            addressAddEditPresenter.View.House = address.House;
            addressAddEditPresenter.Role = Role;

            if (((Form)addressAddEditPresenter.View).ShowDialog() == DialogResult.OK)
            {
                try
                {
                    address.CityFk = addressAddEditPresenter.CityFk;
                    address.StreetFk = addressAddEditPresenter.StreetFk;
                    address.House = addressAddEditPresenter.View.House;
                    Model.AddressRepository.Update(address);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("There is an error by editing an address.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Utils.Log.Trace(ex);
                }
            }

            // notify subscribers about what have changed
            ModifiedHandler();

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

            if (selectedIndices.Count == 0 || MessageBox.Show("Not only these addresses, but all employees with these addresses will be removed" +
                "\n\nAre you sure you want to remove these addresses?", "Addresses removing",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                return;

            try
            {
                for (int i = selectedIndices.Count - 1; i >= 0; i--)
                {
                    Model.AddressRepository.Delete(((Address)View.ListView.Items[selectedIndices[i]].Tag).Id);
                    View.ListView.Items.RemoveAt(selectedIndices[i]);
                }

                View.ListView.Focus();

                // notify subscribers about what have changed
                ModifiedHandler();
            }
            catch (Exception ex)
            {
                MessageBox.Show("There is an error by removing an address.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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