using System;
using System.Collections.Generic;
using Employees.MVP;
using Employees.Views;
using Employees.DomainModel;
using Employees.Utilities;
using System.Windows.Forms;

namespace Employees.Presenters
{
    public class PositionsPresenter : BasePresenter<IPositionView>
    {
        private IMyEmployeesDomainModel Model { get; set; }
        public string Role { get; set; }

        public PositionsPresenter(IMyEmployeesDomainModel domainModel, IPositionView positionView)
        {
            // save references on model and view
            Model = domainModel;
            View = positionView;
            // subscribe to the event of view
            View.Loaded += OnLoaded;
            View.Add += OnAdd;
            View.Edit += OnEdit;
            View.Delete += OnDelete;
            View.ListViewSelectedIndexChanged += OnListView1SelectedIndexChanged;
        }

        // Event for the notification of subscribers, that any position has been edited or deleted
        // It is needed for the immediate update of list of employees

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
                IEnumerable<Position> positions = Model.PositionRepository.GetAll();

                View.ListView.Items.Clear();

                foreach (var position in positions)
                {
                    ListViewItem item = View.ListView.Items.Add(new ListViewItem());
                    item.Text = position.Name;
                    item.SubItems.Add(position.Salary.ToString());

                    item.Tag = position;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There is an error by getting positions.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            var positionItemForm = new PositionItemForm();
            positionItemForm.Text = "New position";
            positionItemForm.GroupBoxText = "Information about new position";

            int selectedIndex = 0;

            if (View.ListView.SelectedIndices.Count != 0)
                selectedIndex = View.ListView.SelectedIndices[0];

            if (positionItemForm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var newPosition = new Position() { Id = 0, Name = positionItemForm.TextBoxPosition, Salary = Int64.Parse(positionItemForm.TextBoxSalary) };
                    Model.PositionRepository.Add(newPosition);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("There is an error by adding new position.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            Position position = (Position)View.ListView.Items[selectedIndex].Tag;

            var positionItemForm = new PositionItemForm();
            positionItemForm.Text = "Existing position changing";
            positionItemForm.GroupBoxText = "Information about position";
            positionItemForm.TextBoxPosition = position.Name;
            positionItemForm.TextBoxSalary = position.Salary.ToString();

            if (positionItemForm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    position.Name = positionItemForm.TextBoxPosition;
                    position.Salary = Int64.Parse(positionItemForm.TextBoxSalary);
                    Model.PositionRepository.Update(position);

                    // notify subscribers about what have changed
                    ModifiedHandler();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("There is an error by editing position.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            if (selectedIndices.Count == 0 || MessageBox.Show("Not only these positions, but all employees with these positions will be removed.\n\n" +
                "Are you sure you want to remove these positions?", "Positions removing", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                return;

            try
            {
                for (int i = selectedIndices.Count - 1; i >= 0; i--)
                {
                    Model.PositionRepository.Delete(((Position)View.ListView.Items[selectedIndices[i]].Tag).Id);
                    View.ListView.Items.RemoveAt(selectedIndices[i]);
                }

                View.ListView.Focus();

                // Оповещаем подписчиков о том, что произошли изменения
                ModifiedHandler();
            }
            catch (Exception ex)
            {
                MessageBox.Show("There is an error by removing position.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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