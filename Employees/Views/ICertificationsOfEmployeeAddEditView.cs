using System;
using System.Windows.Forms;
using Employees.MVP;

namespace Employees.Views
{
    public interface ICertificationsOfEmployeeAddEditView : IView
    {
        ComboBox EmployeeComboBox { get; }
        ComboBox CertificationComboBox { get; }
        
        event EventHandler Loaded;
        event EventHandler EmployeeSelected;
        event EventHandler CertificationSelected;
        event EventHandler AddCertification;
        event EventHandler AddEmployee;
    }
}