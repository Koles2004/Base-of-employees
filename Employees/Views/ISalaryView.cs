using System;
using System.Windows.Forms;
using Employees.MVP;

namespace Employees.Views
{
    public interface ISalaryView : IView
    {
        ComboBox PositionsComboBox { get; }
        string SalaryLabel { get; set; }

        event EventHandler Loaded;
        event EventHandler PositionSelected;
    }
}