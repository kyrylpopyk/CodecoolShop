using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Codecool.CodecoolShop.Core.Models;
using EFCoreInMemory;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class ProductDaoMemory : IProductDao //TODO ask if singleton should be used here
    {
        private readonly InMemoryDb _db;

        public ProductDaoMemory(InMemoryDb db)
        {
            _db = db;
        }

        public void Add(Product item)
        {
            _db.Add(item);
        }

        public void Remove(int id)
        {
            _db.Remove(this.Get(id));
        }

        public Product Get(int id)
        {
            return _db.Products.FirstOrDefault(p => p.Id);
        }

        public IEnumerable<Product> GetAll()
        {
            return data;
        }

        public IEnumerable<Product> GetBy(Supplier supplier)
        {
            return data.Where(x => x.Supplier.Id == supplier.Id);
        }

        public IEnumerable<Product> GetBy(ProductCategory productCategory)
        {
            return data.Where(x => x.ProductCategory.Id == productCategory.Id);
        }
    }
}
