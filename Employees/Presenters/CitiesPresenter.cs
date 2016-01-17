using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Employees.DomainModel;
using Employees.MVP;
using Employees.Utilities;
using Employees.Views;

namespace Employees.Presenters
{
    public class CitiesPresenter : BasePresenter<IRefView>
    {
        private IMyEmployeesDomainModel Model { get; set; }
        public string Role { get; set; }

        public CitiesPresenter(IMyEmployeesDomainModel domainModel, IRefView refView)
        {
            // save references on model and view
            Model = domainModel;
            View = refView;
            View.Title = "Cities";
            // subscribe to the event of view
            View.Loaded += OnLoaded;
            View.Add += OnAdd;
            View.Edit += OnEdit;
            View.Delete += OnDelete;
            View.ListViewSelectedIndexChanged += OnListView1SelectedIndexChanged;
        }

        // Event for the notification of subscribers, that any city has been edited or deleted
        // It is needed for the immediate update of list of addresses

        public event EventHandler Modified;

        private void ModifiedHandler()
        {
            if (Modified != null)
                Modified(this, EventArgs.Empty);
        }

        // Clear the list in the view and fill it again from the model
        private void UpdateView()
        {
            try
            {
                IEnumerable<City> cities = Model.CityRepository.GetAll();

                View.ListView.Items.Clear();

                foreach (var city in cities)
                {
                    ListViewItem item = View.ListView.Items.Add(new ListViewItem());
                    item.Text = city.Name;
                    item.Tag = city;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There is an error by getting cities.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Utils.Log.Trace(ex);
            }

            View.ListView.Focus();
        }

        private void OnLoaded(object sender, EventArgs e)
        {
            UpdateView();
        }

        private void OnAdd(object sender, EventArgs e)
        {
            var refItemForm = new RefItemForm();
            refItemForm.Text = "New city";
            refItemForm.GroupBoxText = "Information about new city";
            refItemForm.LabelText = "City:";

            int selectedIndex = 0;

            if (View.ListView.SelectedIndices.Count != 0)
                selectedIndex = View.ListView.SelectedIndices[0];

            if (refItemForm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var newCity = new City() { Id = 0, Name = refItemForm.TextBoxText };
                    Model.CityRepository.Add(newCity);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("There is an error by adding new city.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Utils.Log.Trace(ex);
                }
            }

            ListView.ListViewItemCollection collection = View.ListView.Items;
            int count = collection.Count;

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
            var selectedIndex = View.ListView.SelectedIndices[0];
            City city = (City)View.ListView.Items[selectedIndex].Tag;

            var refItemForm = new RefItemForm();
            refItemForm.Text = "Existing city changing";
            refItemForm.GroupBoxText = "Information about city";
            refItemForm.LabelText = "City:";
            refItemForm.TextBoxText = city.Name;

            if (refItemForm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    city.Name = refItemForm.TextBoxText;
                    Model.CityRepository.Update(city);

                    // notify subscribers about what have changed
                    ModifiedHandler();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("There is an error by editing city.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Utils.Log.Trace(ex);
                }
            }

            UpdateView();

            // select element in ListView, which was changing
            ListView.ListViewItemCollection collection = View.ListView.Items;
            View.ListView.Select();
            collection[selectedIndex].Selected = true;
            View.ListView.EnsureVisible(selectedIndex);
        }

        private void OnDelete(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection selectedIndices = View.ListView.SelectedIndices;

            if (selectedIndices.Count == 0 || MessageBox.Show("Not only these cities, but all addresses with these cities," +
                " and all employees with these addresses will be removed.\n\n"+
                "Are you sure you want to remove these cities?", "Cities removing", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                return;

            try
            {
                for (int i = selectedIndices.Count - 1; i >= 0; i--)
                {
                    Model.CityRepository.Delete(((City)View.ListView.Items[selectedIndices[i]].Tag).Id);
                    View.ListView.Items.RemoveAt(selectedIndices[i]);
                }

                View.ListView.Focus();

                // notify subscribers about what have changed
                ModifiedHandler();
            }
            catch (Exception ex)
            {
                MessageBox.Show("There is an error by removing city.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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