namespace Employees.DomainModel
{
    public class CertificationsOfEmployee
    {
        public long Id { get; set; }
        public long EmployeeFk { get; set; }
        public long CertificationFk { get; set; }

        // EF
        public virtual Employee Employee { get; set; } // navigation property for EmployeeFk
        public virtual Certification Certification { get; set; } // navigation property for CertificationFk
    }
}