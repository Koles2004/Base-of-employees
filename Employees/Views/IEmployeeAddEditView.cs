using System;
using System.Windows.Forms;
using Employees.MVP;

namespace Employees.Views
{
    public interface IEmployeeAddEditView : IView
    {
        string Surname { get; set; }
        string NameOfEmployee { get; set; }
        string Patronymic { get; set; }
        string Phone { get; set; }
        DateTime DateOfBirth { get; set; }
        ComboBox AddressComboBox { get; }
        ComboBox PositionComboBox { get; }
        ComboBox ComboBoxSex { get; }
        string GroupBoxText { set; }
        PictureBox PictureBoxPhoto { get; }
        Button ButtonDeletePhoto { get; }
        
        event EventHandler Loaded;
        event EventHandler SexSelected;
        event EventHandler AddressSelected;
        event EventHandler PositionSelected;
        event EventHandler PhotosOpened;
        event EventHandler PhotoCleared;
        event EventHandler AddAddress;
        event EventHandler AddPosition;
    }
}