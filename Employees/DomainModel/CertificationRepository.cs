using System.Collections.Generic;

namespace Employees.DomainModel
{
    public class CertificationRepository : IRepository<Certification>
    {
        private readonly IDatabase db;

        public CertificationRepository(IDatabase db)
        {
            this.db = db;
        }

        public IEnumerable<Certification> GetAll()
        {
            return db.GetAllCertifications();
        }

        public Certification GetById(long id)
        {
            return db.GetCertificationById(id);
        }

        public long Add(Certification entity)
        {
            return db.InsertCertification(entity);
        }

        public void Update(Certification entity)
        {
            db.UpdateCertification(entity);
        }

        public void Delete(long id)
        {
            db.DeleteCertification(id);
        }
    }
}