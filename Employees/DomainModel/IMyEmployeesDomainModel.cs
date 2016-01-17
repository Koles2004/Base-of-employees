using System;

namespace Employees.DomainModel
{
    public interface IMyEmployeesDomainModel : IDisposable
    {
        IRepository<Address> AddressRepository { get; }
        IRepository<City> CityRepository { get; }
        IRepository<Street> StreetRepository { get; }
        IRepository<Position> PositionRepository { get; }
        IRepository<Certification> CertificationRepository { get; }
        IRepository<CertificationsOfEmployee> CertificationOfEmployeeRepository { get; }
        IRepository<Employee> EmployeeRepository { get; }
    }
}