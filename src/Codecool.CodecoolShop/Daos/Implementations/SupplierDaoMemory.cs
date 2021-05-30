using System.Collections.Generic;
using System.Linq;
using Codecool.CodecoolShop.Core.Models;
using EFCoreInMemory;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class SupplierDaoMemory : ISupplierDao
    {
        private readonly InMemoryDb _db;

        public SupplierDaoMemory(InMemoryDb db)
        {
            _db = db;
        }

        public void Add(Supplier item)
        {
            _db.Add(item);
        }

        public void Remove(int id)
        {
            //data.Remove(this.Get(id));
            _db.Suppliers.Remove(Get(id));
        }

        public Supplier Get(int id)
        {
            //return data.Find(x => x.Id == id);
            return _db.Suppliers.FirstOrDefault(s => s.Id == id); // TODO: is it okay?
        }

        public IEnumerable<Supplier> GetAll()
        {
            return _db.Suppliers;
        }
    }
}
