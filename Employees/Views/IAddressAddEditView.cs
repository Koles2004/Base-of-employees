using System;
using System.Windows.Forms;
using Employees.MVP;

namespace Employees.Views
{
    public interface IAddressAddEditView: IView
    {
        ComboBox CityComboBox { get; }
        ComboBox StreetComboBox { get; }
        string House { get; set; }
        
        event EventHandler Loaded;
        event EventHandler CitySelected;
        event EventHandler StreetSelected;
        event EventHandler AddCity;
        event EventHandler AddStreet;
    }
}