using System.Collections.Generic;

namespace Employees.DomainModel
{
    public class StreetRepository : IRepository<Street>
    {
        private readonly IDatabase db;

        public StreetRepository(IDatabase db)
        {
            this.db = db;
        }

        public IEnumerable<Street> GetAll()
        {
            return db.GetAllStreets();
        }

        public Street GetById(long id)
        {
            return db.GetStreetById(id);
        }

        public long Add(Street entity)
        {
            return db.InsertStreet(entity);
        }

        public void Update(Street entity)
        {
            db.UpdateStreet(entity);
        }

        public void Delete(long id)
        {
            db.DeleteStreet(id);
        }
    }
}