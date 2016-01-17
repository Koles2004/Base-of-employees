using System;
using System.Windows.Forms;
using Employees.MVP;

namespace Employees.Views
{
    public interface ICertificationsOfEmployeeView : IView
    {
        ListView ListView { get; }

        Button AddButton { get; set; }
        Button EditButton { get; set; }
        Button DeleteButton { get; set; }

        event EventHandler Loaded;
        event EventHandler Add;
        event EventHandler Edit;
        event EventHandler Delete;
        
        event EventHandler ListViewSelectedIndexChanged;
    }
}