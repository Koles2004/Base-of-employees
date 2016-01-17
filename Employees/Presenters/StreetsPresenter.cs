using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Employees.DomainModel;
using Employees.MVP;
using Employees.Utilities;
using Employees.Views;

namespace Employees.Presenters
{
    public class StreetsPresenter : BasePresenter<IRefView>
    {
        private IMyEmployeesDomainModel Model { get; set; }
        public string Role { get; set; }

        public StreetsPresenter(IMyEmployeesDomainModel domainModel, IRefView refView)
        {
            // save references on model and view
            Model = domainModel;
            View = refView;
            View.Title = "Streets";
            // subscribe to the event of view
            View.Loaded += OnLoaded;
            View.Add += OnAdd;
            View.Edit += OnEdit;
            View.Delete += OnDelete;
            View.ListViewSelectedIndexChanged += OnListView1SelectedIndexChanged;
        }

        // Event for the notification of subscribers, that any street has been edited or deleted
        // It is needed for the immediate update of list of addresses

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
                IEnumerable<Street> streets = Model.StreetRepository.GetAll();

                View.ListView.Items.Clear();

                foreach (var street in streets)
                {
                    ListViewItem item = View.ListView.Items.Add(new ListViewItem());
                    item.Text = street.Name;
                    item.Tag = street;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There is an error by getting streets.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            refItemForm.Text = "New street";
            refItemForm.GroupBoxText = "Information about street";
            refItemForm.LabelText = "Street:";

            int selectedIndex = 0;

            if (View.ListView.SelectedIndices.Count != 0)
                selectedIndex = View.ListView.SelectedIndices[0];

            if (refItemForm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var newStreet = new Street() { Id = 0, Name = refItemForm.TextBoxText };
                    Model.StreetRepository.Add(newStreet);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("There is an error by adding new street.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            Street street = (Street)View.ListView.Items[selectedIndex].Tag;

            var refItemForm = new RefItemForm();
            refItemForm.Text = "Existing street changing";
            refItemForm.GroupBoxText = "Information about street";
            refItemForm.LabelText = "Street:";
            refItemForm.TextBoxText = street.Name;

            if (refItemForm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    street.Name = refItemForm.TextBoxText;
                    Model.StreetRepository.Update(street);

                    // notify subscribers about what have changed
                    ModifiedHandler();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("There is an error by editing street.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            if (selectedIndices.Count == 0 || MessageBox.Show("Not only these streets, but all addresses with these streets," +
                " and all employees with these addresses will be removed.\n\n" +
                "Are you sure you want to remove these streets?", "Streets removing", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                return;

            try
            {
                for (int i = selectedIndices.Count - 1; i >= 0; i--)
                {
                    Model.StreetRepository.Delete(((Street)View.ListView.Items[selectedIndices[i]].Tag).Id);
                    View.ListView.Items.RemoveAt(selectedIndices[i]);
                }

                View.ListView.Focus();

                // notify subscribers about what have changed
                ModifiedHandler();
            }
            catch (Exception ex)
            {
                MessageBox.Show("There is an error by removing street.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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