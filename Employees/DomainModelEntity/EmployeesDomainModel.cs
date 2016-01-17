using Employees.DomainModel;

namespace Employees.DomainModelEntity
{
    public class EmployeesDomainModel : IMyEmployeesDomainModel
    {
        private readonly IDatabase db;

        private readonly IRepository<Address> addressRepository;
        private readonly IRepository<City> cityRepository;
        private readonly IRepository<Street> streetRepository;
        private readonly IRepository<Position> positionRepository;
        private readonly IRepository<Certification> certificationRepository;
        private readonly IRepository<CertificationsOfEmployee> certificationsOfEmployeeRepository;
        private readonly IRepository<Employee> employeeRepository;

        public IRepository<Address> AddressRepository { get { return addressRepository; } }
        public IRepository<City> CityRepository { get { return cityRepository; } }
        public IRepository<Street> StreetRepository { get { return streetRepository; } }
        public IRepository<Position> PositionRepository { get { return positionRepository; } }
        public IRepository<Certification> CertificationRepository { get { return certificationRepository; } }
        public IRepository<CertificationsOfEmployee> CertificationOfEmployeeRepository { get { return certificationsOfEmployeeRepository; } }
        public IRepository<Employee> EmployeeRepository { get { return employeeRepository; } }

        public EmployeesDomainModel()
        {
            db = new Database();
            addressRepository = new AddressRepository(db);
            cityRepository = new CityRepository(db);
            streetRepository = new StreetRepository(db);
            positionRepository = new PositionRepository(db);
            certificationRepository = new CertificationRepository(db);
            certificationsOfEmployeeRepository = new CertificationsOfEmployeeRepository(db);
            employeeRepository = new EmployeeRepository(db);
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}