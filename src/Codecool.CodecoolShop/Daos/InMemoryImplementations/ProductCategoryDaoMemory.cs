using System.Collections.Generic;
using System.Linq;
using Codecool.CodecoolShop.Core.Models;
using EFCoreInMemory;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    class ProductCategoryDaoMemory : IProductCategoryDao
    {
        private readonly InMemoryDb _db;

        public ProductCategoryDaoMemory(InMemoryDb db)
        {
            _db = db;
        }

        public void Add(ProductCategory item)
        {
            _db.ProductCategories.Add(item);
        }

        public void Remove(int id)
        {
            _db.ProductCategories.Remove(Get(id));
        }

        public ProductCategory Get(int id)
        {
            return _db.ProductCategories.FirstOrDefault(pC => pC.Id == id);
        }

        public IEnumerable<ProductCategory> GetAll()
        {
            return _db.ProductCategories.ToList();
        }
    }
}
