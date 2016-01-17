using System.Collections.Generic;

namespace Employees.DomainModel
{
    public class Certification
    {
        public long Id { get; set; }
        public string Name { get; set; }

        // EF
        public ICollection<CertificationsOfEmployee> CertificationsOfEmployees { get; set; } // CertificationsOfEmployee(s) of this certification
    }
}