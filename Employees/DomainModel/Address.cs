using System.Collections.Generic;

namespace Employees.DomainModel
{
    public class Address
    {
        public long Id { get; set; }
        public long CityFk { get; set; }
        public long StreetFk { get; set; }
        public string House { get; set; }

        // Entity Framework
        public ICollection<Employee> Employees { get; set; } // employees with this address
        public virtual City City { get; set; } // navigation property for CityFk
        public virtual Street Street { get; set; } // navigation property for StreetFk
    }
}