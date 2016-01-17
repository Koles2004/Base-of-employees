using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Employees.DomainModel;

namespace Employees.DomainModelEntity
{
    public class Database : IDatabase
    {
        private readonly EmployeesContext context = new EmployeesContext();

        public void Dispose()
        {
            context.Dispose();
        }

#region Addresses management

        public IEnumerable<Address> GetAllAddresses()
        {
            return context.Addresses.ToList();
        }

        public Address GetAddressById(long id)
        {
            return context.Addresses.Find(id);
        }

        public long InsertAddress(Address address)
        {
            context.Addresses.Add(address);
            context.SaveChanges();

            return address.Id;
        }

        public void UpdateAddress(Address address)
        {
            Address a = context.Addresses.Find(address.Id);

            if (a != null)
            {
                context.Entry(a).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteAddress(long id)
        {
            Address address = context.Addresses.Find(id);

            if (address != null)
            {
                context.Addresses.Remove(address);
                context.SaveChanges();
            }
        }
#endregion

#region Streets management

        public IEnumerable<Street> GetAllStreets()
        {
            return context.Streets.ToList();
        }

        public Street GetStreetById(long id)
        {
            return context.Streets.Find(id);
        }

        public long InsertStreet(Street street)
        {
            context.Streets.Add(street);
            context.SaveChanges();

            return street.Id;
        }

        public void UpdateStreet(Street street)
        {
            Street s = context.Streets.Find(street.Id);

            if (s != null)
            {
                context.Entry(s).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteStreet(long id)
        {
            Street street = context.Streets.Find(id);

            if (street != null)
            {
                context.Streets.Remove(street);
                context.SaveChanges();
            }
        }
#endregion

#region Cities management

        public IEnumerable<City> GetAllCities()
        {
            return context.Cities.ToList();
        }

        public City GetCityById(long id)
        {
            return context.Cities.Find(id);
        }

        public long InsertCity(City city)
        {
            context.Cities.Add(city);
            context.SaveChanges();

            return city.Id;
        }

        public void UpdateCity(City city)
        {
            City c = context.Cities.Find(city.Id);

            if (c != null)
            {
                context.Entry(c).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteCity(long id)
        {
            City city = context.Cities.Find(id);

            if (city != null)
            {
                context.Cities.Remove(city);
                context.SaveChanges();
            }
        }
#endregion

#region Positions management

        public IEnumerable<Position> GetAllPositions()
        {
            return context.Positions.ToList();
        }

        public Position GetPositionById(long id)
        {
            return context.Positions.Find(id);
        }

        public long InsertPosition(Position position)
        {
            context.Positions.Add(position);
            context.SaveChanges();

            return position.Id;
        }

        public void UpdatePosition(Position position)
        {
            Position p = context.Positions.Find(position.Id);

            if (p != null)
            {
                context.Entry(p).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeletePosition(long id)
        {
            Position position = context.Positions.Find(id);

            if (position != null)
            {
                context.Positions.Remove(position);
                context.SaveChanges();
            }
        }
#endregion

#region Certifications

        public IEnumerable<Certification> GetAllCertifications()
        {
            return context.Certifications.ToList();
        }

        public Certification GetCertificationById(long id)
        {
            return context.Certifications.Find(id);
        }

        public long InsertCertification(Certification certification)
        {
            context.Certifications.Add(certification);
            context.SaveChanges();

            return certification.Id;
        }

        public void UpdateCertification(Certification certification)
        {
            Certification c = context.Certifications.Find(certification.Id);

            if (c != null)
            {
                context.Entry(c).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteCertification(long id)
        {
            Certification certification = context.Certifications.Find(id);

            if (certification != null)
            {
                context.Certifications.Remove(certification);
                context.SaveChanges();
            }
        }
#endregion

#region Employees

        public IEnumerable<Employee> GetAllEmployees()
        {
            return context.Employees.ToList();
        }

        public Employee GetEmployeeById(long id)
        {
            return context.Employees.Find(id);
        }

        public long InsertEmployee(Employee employee)
        {
            context.Employees.Add(employee);
            context.SaveChanges();

            return employee.Id;
        }

        public void UpdateEmployee(Employee employee)
        {
            Employee e = context.Employees.Find(employee.Id);

            if (e != null)
            {
                context.Entry(e).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteEmployee(long id)
        {
            Employee employee = context.Employees.Find(id);

            if (employee != null)
            {
                context.Employees.Remove(employee);
                context.SaveChanges();
            }
        }
#endregion

#region CertificationsOfEmployees

        public IEnumerable<CertificationsOfEmployee> GetAllCertificationsOfEmployees()
        {
            return context.CertificationsOfEmployees.ToList();
        }

        public CertificationsOfEmployee GetCertificationsOfEmployeeById(long id)
        {
            return context.CertificationsOfEmployees.Find(id);
        }

        public long InsertCertificationsOfEmployee(CertificationsOfEmployee certificationsOfEmployee)
        {
            context.CertificationsOfEmployees.Add(certificationsOfEmployee);
            context.SaveChanges();

            return certificationsOfEmployee.Id;
        }

        public void UpdateCertificationsOfEmployee(CertificationsOfEmployee certificationsOfEmployee)
        {
            CertificationsOfEmployee c = context.CertificationsOfEmployees.Find(certificationsOfEmployee.Id);

            if (c != null)
            {
                context.Entry(c).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteCertificationsOfEmployee(long id)
        {
            CertificationsOfEmployee certificationsOfEmployee = context.CertificationsOfEmployees.Find(id);

            if (certificationsOfEmployee != null)
            {
                context.CertificationsOfEmployees.Remove(certificationsOfEmployee);
                context.SaveChanges();
            }
        }
#endregion
    }
}