using System;
using System.Collections.Generic;

namespace Employees.DomainModel
{
    public class Employee
    {
        public long Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Sex { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public long AddressFk { get; set; }
        public long PositionFk { get; set; }
        public string Photo { get; set; }

        // EF
        public ICollection<CertificationsOfEmployee> CertificationsOfEmployees { get; set; } // CertificationsOfEmployee(s) of this employee
        public virtual Address Address { get; set; } // navigation property for AddressFk
        public virtual Position Position { get; set; } // navigation property for PositionFk
    }
}