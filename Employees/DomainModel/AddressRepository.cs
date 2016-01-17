using System.Collections.Generic;

namespace Employees.DomainModel
{
    public class AddressRepository : IRepository<Address>
    {
        private readonly IDatabase db;

        public AddressRepository(IDatabase db)
        {
            this.db = db;
        }

        public IEnumerable<Address> GetAll()
        {
            return db.GetAllAddresses();
        }

        public Address GetById(long id)
        {
            return db.GetAddressById(id);
        }

        public long Add(Address entity)
        {
            return db.InsertAddress(entity);
        }

        public void Update(Address entity)
        {
            db.UpdateAddress(entity);
        }

        public void Delete(long id)
        {
            db.DeleteAddress(id);
        }
    }
}