using System.Collections.Generic;

namespace Employees.DomainModel
{
    public class Street
    {
        public long Id { get; set; }
        public string Name { get; set; }

        // EF
        public ICollection<Address> Addresses { get; set; } // addresses of this street
    }
}