using System.Collections.Generic;

namespace Employees.DomainModel
{
    public class CityRepository : IRepository<City>
    {
        private readonly IDatabase db;

        public CityRepository(IDatabase db)
        {
            this.db = db;
        }

        public IEnumerable<City> GetAll()
        {
            return db.GetAllCities();
        }

        public City GetById(long id)
        {
            return db.GetCityById(id);
        }

        public long Add(City entity)
        {
            return db.InsertCity(entity);
        }

        public void Update(City entity)
        {
            db.UpdateCity(entity);
        }

        public void Delete(long id)
        {
            db.DeleteCity(id);
        }
    }
}