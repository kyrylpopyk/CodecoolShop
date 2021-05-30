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
            return _db.Products.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Product> GetAll()
        {
            return _db.Products.ToList();
        }

        public IEnumerable<Product> GetBy(Supplier supplier)
        {
            return _db.Products.Where(p => p.Supplier.Id == supplier.Id);
        }

        public IEnumerable<Product> GetBy(ProductCategory productCategory)
        {
            return _db.Products.Where(p => p.ProductCategory.Id == productCategory.Id);
            return data.Where(x => x.ProductCategory.Id == productCategory.Id);
        }
    }
}
