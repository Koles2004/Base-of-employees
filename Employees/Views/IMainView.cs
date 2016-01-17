using System;
using System.Windows.Forms;
using Employees.MVP;

namespace Employees.Views
{
    public interface IMainView : IView
    {
        ListView ListView { get; }

        Button AddButton { get; set; }
        Button EditButton { get; set; }
        Button DeleteButton { get; set; }
        Button ButtonOpenPhoto { get; }
        Button ButtonShowCertifications { get; }
        
        String SurnameTextBox { get; set; }
        String NameTextBox { get; set; }
        String PatronymicTextBox { get; set; }
        String PositionTextBox { get; set; }
        ComboBox SexComboBox { get; }

        event EventHandler Loaded;
        event EventHandler Add;
        event EventHandler Edit;
        event EventHandler Delete;

        event EventHandler EditCities;
        event EventHandler EditStreets;
        event EventHandler EditAddresses;

        event EventHandler EditPositions;
        event EventHandler EditCertifications;
        event EventHandler EditCertificationsOfEmployees;

        event EventHandler ShowSalary;
        event EventHandler OpenPhoto;
        event EventHandler OpenAboutForm;
        event EventHandler SearchEmployees;
        event EventHandler ShowAllEmployees;
        event EventHandler ShowCertificationsOfEmployee;

        event EventHandler ListViewSelectedIndexChanged;
    }
}