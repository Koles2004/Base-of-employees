using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Employees.DomainModel;
using Employees.MVP;
using Employees.Utilities;
using Employees.Views;

namespace Employees.Presenters
{
    public class SalaryPresenter : BasePresenter<ISalaryView>
    {
        private IMyEmployeesDomainModel Model { get; set; }
        private List<Position> Positions { get; set; }
        private List<Employee> Employees { get; set; } 
        public long PositionFk { get; set; }

        public SalaryPresenter(IMyEmployeesDomainModel domainModel, ISalaryView salaryView)
        {
            Model = domainModel;
            View = salaryView;
            View.Loaded += OnLoaded;
            View.PositionSelected += OnPositionSelected;
        }

        private void OnLoaded(object sender, EventArgs e)
        {
            Employees = Model.EmployeeRepository.GetAll().ToList();

            try
            {
                Positions = Model.PositionRepository.GetAll().ToList();
                foreach (var position in Positions)
                {
                    View.PositionsComboBox.Items.Add(position.Name);
                }

                View.PositionsComboBox.Items.Add("All employees");

                View.PositionsComboBox.SelectedIndex = PositionFk == 0 ? 0 : Positions.FindIndex(p => p.Id == PositionFk);
            }
            catch (Exception ex)
            {
                MessageBox.Show("There is an error by getting positions.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Utils.Log.Trace(ex);
            }
        }

        private void OnPositionSelected(object sender, EventArgs e)
        {
            long count = 0;
            long money = 0;

            if (View.PositionsComboBox.SelectedIndex != View.PositionsComboBox.Items.Count - 1)
            {
                // When the user selects from the drop-down list another position, remember its Id
                PositionFk = Positions[View.PositionsComboBox.SelectedIndex].Id;

                count = Employees.Count(em => em.PositionFk == PositionFk);
                money = count * Positions.Find(p => p.Id == PositionFk).Salary;
            }
            else
            {
                count = Employees.Count;

                foreach (var emp in Employees)
                {
                    money += Positions.Find(p => p.Id == emp.PositionFk).Salary;
                }

                // LINQ
                //money += Employees.Sum(emp => Positions.Find(p => p.Id == emp.PositionFk).Salary);
            }

            View.SalaryLabel = "Count - " + count + ".  Salary per month - " + money + " hrn.";
        }
    }
}