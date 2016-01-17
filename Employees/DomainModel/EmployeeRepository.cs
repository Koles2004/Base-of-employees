using System.Collections.Generic;

namespace Employees.DomainModel
{
    public class EmployeeRepository : IRepository<Employee>
    {
        private readonly IDatabase db;

        public EmployeeRepository(IDatabase db)
        {
            this.db = db;
        }

        public IEnumerable<Employee> GetAll()
        {
            return db.GetAllEmployees();
        }

        public Employee GetById(long id)
        {
            return db.GetEmployeeById(id);
        }

        public long Add(Employee entity)
        {
            return db.InsertEmployee(entity);
        }

        public void Update(Employee entity)
        {
            db.UpdateEmployee(entity);
        }

        public void Delete(long id)
        {
            db.DeleteEmployee(id);
        }
    }
}