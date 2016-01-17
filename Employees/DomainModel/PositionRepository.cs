using System.Collections.Generic;

namespace Employees.DomainModel
{
    public class PositionRepository : IRepository<Position>
    {
        private readonly IDatabase db;

        public PositionRepository(IDatabase db)
        {
            this.db = db;
        }

        public IEnumerable<Position> GetAll()
        {
            return db.GetAllPositions();
        }

        public Position GetById(long id)
        {
            return db.GetPositionById(id);
        }

        public long Add(Position entity)
        {
            return db.InsertPosition(entity);
        }

        public void Update(Position entity)
        {
            db.UpdatePosition(entity);
        }

        public void Delete(long id)
        {
            db.DeletePosition(id);
        }
    }
}