using System.Collections.Generic;

namespace Employees.DomainModel
{
    public class Position
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long Salary { get; set; }

        // EF
        public ICollection<Employee> Employees { get; set; } // employees of this position
    }
}