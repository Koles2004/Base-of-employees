using System;
using System.Collections.Generic;

namespace Employees.DomainModel
{
    public interface IDatabase : IDisposable
    {
        IEnumerable<Address> GetAllAddresses();
        Address GetAddressById(long id);
        long InsertAddress(Address address);
        void UpdateAddress(Address address);
        void DeleteAddress(long id);

        IEnumerable<Street> GetAllStreets();
        Street GetStreetById(long id);
        long InsertStreet(Street street);
        void UpdateStreet(Street street);
        void DeleteStreet(long id);

        IEnumerable<City> GetAllCities();
        City GetCityById(long id);
        long InsertCity(City city);
        void UpdateCity(City city);
        void DeleteCity(long id);

        IEnumerable<Position> GetAllPositions();
        Position GetPositionById(long id);
        long InsertPosition(Position position);
        void UpdatePosition(Position position);
        void DeletePosition(long id);

        IEnumerable<Certification> GetAllCertifications();
        Certification GetCertificationById(long id);
        long InsertCertification(Certification certification);
        void UpdateCertification(Certification certification);
        void DeleteCertification(long id);

        IEnumerable<Employee> GetAllEmployees();
        Employee GetEmployeeById(long id);
        long InsertEmployee(Employee employee);
        void UpdateEmployee(Employee employee);
        void DeleteEmployee(long id);

        IEnumerable<CertificationsOfEmployee> GetAllCertificationsOfEmployees();
        CertificationsOfEmployee GetCertificationsOfEmployeeById(long id);
        long InsertCertificationsOfEmployee(CertificationsOfEmployee certificationsOfEmployee);
        void UpdateCertificationsOfEmployee(CertificationsOfEmployee certificationsOfEmployee);
        void DeleteCertificationsOfEmployee(long id);
    }
}