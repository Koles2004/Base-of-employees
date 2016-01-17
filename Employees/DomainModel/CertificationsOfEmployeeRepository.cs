using System.Collections.Generic;

namespace Employees.DomainModel
{
    public class CertificationsOfEmployeeRepository : IRepository<CertificationsOfEmployee>
    {
        private readonly IDatabase db;

        public CertificationsOfEmployeeRepository(IDatabase db)
        {
            this.db = db;
        }

        public IEnumerable<CertificationsOfEmployee> GetAll()
        {
            return db.GetAllCertificationsOfEmployees();
        }

        public CertificationsOfEmployee GetById(long id)
        {
            return db.GetCertificationsOfEmployeeById(id);
        }

        public long Add(CertificationsOfEmployee entity)
        {
            return db.InsertCertificationsOfEmployee(entity);
        }

        public void Update(CertificationsOfEmployee entity)
        {
            db.UpdateCertificationsOfEmployee(entity);
        }

        public void Delete(long id)
        {
            db.DeleteCertificationsOfEmployee(id);
        }
    }
}