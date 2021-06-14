using EFDataAccessLibrary.Models;
using System.Collections.Generic;

namespace Codecool.CodecoolShop.Daos
{
    public interface IProductDao : IDao<Product>
    {
        IEnumerable<Product> GetBy(Supplier supplier);
        IEnumerable<Product> GetBy(ProductCategory productCategory);
        public IEnumerable<Product> GetBySupplierAndCategory(Supplier supplier, ProductCategory category);
    }
}
