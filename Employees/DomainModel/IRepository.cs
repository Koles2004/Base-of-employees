using System.Collections.Generic;

namespace Employees.DomainModel
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T GetById(long id);
        long Add(T entity);
        void Update(T entity);
        void Delete(long id);
    }
}