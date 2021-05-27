using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Codecool.CodecoolShop.InMemoreEF;
using Codecool.CodecoolShop.Models;
using Microsoft.EntityFrameworkCore;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class ProductDao : IProductDao
    {
        private InMemoryDbContext _db;

        public ProductDao(InMemoryDbContext db) // TODO decouple from DbContext implementation
        {
            _db = db;
        }

        public void Add(Product item)
        {
            _db.Products.Add(item);
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Product Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetBy(Supplier supplier)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetBy(ProductCategory productCategory)
        {
            throw new NotImplementedException();
        }
    }
}
