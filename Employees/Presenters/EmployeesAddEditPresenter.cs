using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Employees.DomainModel;
using Employees.MVP;
using Employees.Utilities;
using Employees.Views;

namespace Employees.Presenters
{
    public class EmployeesAddEditPresenter : BasePresenter<IEmployeeAddEditView>
    {
        private IMyEmployeesDomainModel Model { get; set; }
        private string[] sex = {"Male", "Female"};

        private List<Address> Addresses { get; set; }
        private List<City> Cities { get; set; }
        private List<Street> Streets { get; set; }
        private List<Position> Positions { get; set; }
        public long AddressFk { get; set; }
        public long PositionFk { get; set; }
        public long Item { get; set; }
        public string Title { set { ((Form)View).Text = value; } }
        public string GroupBoxText { set { View.GroupBoxText = value; } }
        public string FileName { get; set; }
        string pathForPhoto = String.Empty;
        public string Role { get; set; }

        public EmployeesAddEditPresenter(IMyEmployeesDomainModel domainModel, IEmployeeAddEditView employeeAddEditView, string handler)
        {
            Model = domainModel;
            View = employeeAddEditView;
            View.Loaded += OnLoaded;

            if (handler == "New")
                View.SexSelected += OnSexSelected;
            else
            {
                View.SexSelected += OnNotSexSelected;
            }
            View.AddressSelected += OnAddressSelected;
            View.PositionSelected += OnPositionSelected;
            View.PhotosOpened += OnOpenPhoto;
            View.AddPosition += OnAddPosition;
            View.AddAddress += OnAddAddress;
            View.PhotoCleared += OnPhotoCleared;
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

        // it loads photo on the form
        private void LoadPhoto()
        {
            if (FileName != null)
            {
                pathForPhoto = Application.StartupPath + @"\Photos" + @"\" + FileName;
                Image image = Image.FromFile(pathForPhoto);

                View.PictureBoxPhoto.Image = image;
                View.PictureBoxPhoto.SizeMode = PictureBoxSizeMode.StretchImage;
            }

            View.ButtonDeletePhoto.Enabled = FileName != "woman.png" && FileName != "man.jpg";
        }

        private void UpdateView()
        {
            View.AddressComboBox.Items.Clear();
            View.PositionComboBox.Items.Clear();
            View.ComboBoxSex.Items.Clear();

            try
            {
                foreach (var s in sex)
                {
                    View.ComboBoxSex.Items.Add(s);
                }

                View.ComboBoxSex.SelectedIndex = Item == 1 ? 1 : 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("There is an error by getting sexes.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Utils.Log.Trace(ex);
            }

            try
            {
                Addresses = Model.AddressRepository.GetAll().ToList();
                Cities = Model.CityRepository.GetAll().ToList();
                Streets = Model.StreetRepository.GetAll().ToList();
                Positions = Model.PositionRepository.GetAll().ToList();

                foreach (var address in Addresses)
                {
                    View.AddressComboBox.Items.Add((Cities.Find(c => c.Id == address.CityFk).Name) + "   "
                        + (Streets.Find(s => s.Id == address.StreetFk).Name) + "   " + address.House);
                }

                // // there is an address in ComboBox when form is loaded
                View.AddressComboBox.SelectedIndex = AddressFk == 0 ? 0 : Addresses.FindIndex(a => a.Id == AddressFk);

                foreach (var position in Positions)
                {
                    View.PositionComboBox.Items.Add(position.Name);
                }

                // there is a position in ComboBox when form is loaded
                View.PositionComboBox.SelectedIndex = PositionFk == 0 ? 0 : Positions.FindIndex(p => p.Id == PositionFk);
            }
            catch (Exception ex)
            {
                MessageBox.Show("There is an error by getting addresses.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Utils.Log.Trace(ex);
            }
        }

        private void OnLoaded(object sender, EventArgs e)
        {
            UpdateView();
        }

        private void OnSexSelected(object sender, EventArgs e)
        {
            Item = View.ComboBoxSex.SelectedIndex;

            FileName = Item == 1 ? "woman.png" : "man.jpg";

            LoadPhoto();
        }

        private void OnNotSexSelected(object sender, EventArgs e)
        {
            LoadPhoto();
        }

        private void OnAddressSelected(object sender, EventArgs e)
        {
            // When the user selects from the drop-down list another address, remember its Id
            AddressFk = Addresses[View.AddressComboBox.SelectedIndex].Id;
        }

        private void OnPositionSelected(object sender, EventArgs e)
        {
            // When the user selects from the drop-down list another position, remember its Id
            PositionFk = Positions[View.PositionComboBox.SelectedIndex].Id;
        }

        // добавление фотографии
        private void OnOpenPhoto(object sender, EventArgs e)
        {
            MessageBox.Show("Choose photo and push button \"Open\" for adding photo of employee.\n\n" +
                               "If you don't choose a photo, then default photo will be added.", "Photo");

            OpenFileDialog dlg = new OpenFileDialog()
            {
                InitialDirectory = Application.StartupPath + "\\Photos"
            };

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                FileName = dlg.SafeFileName;
            }
            else
            {
                FileName = Item == 1 ? "woman.png" : "man.jpg";
            }

            LoadPhoto();
        }

        private void OnPhotoCleared(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to clear photo?", "Clear photo", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {
                FileName = Item == 1 ? "woman.png" : "man.jpg";
                LoadPhoto();
            }
        }

        private void OnAddPosition(object sender, EventArgs e)
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

            UpdateView();
        }

        private void OnAddAddress(object sender, EventArgs e)
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
    }
}