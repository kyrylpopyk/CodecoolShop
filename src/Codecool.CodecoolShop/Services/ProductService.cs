using System.Collections.Generic;
using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Core.Models;
using System.Linq;

namespace Codecool.CodecoolShop.Services
{
    public class ProductService
    {
        private readonly IProductDao _productDao;
        private readonly IProductCategoryDao _productCategoryDao;
        private readonly ISupplierDao _supplierDao;

        public ProductService(IProductDao productDao, IProductCategoryDao productCategoryDao,
            ISupplierDao suplierDao)
        {
            _productDao = productDao;
            _productCategoryDao = productCategoryDao;
            _supplierDao = suplierDao;
        }

        public ProductCategory GetProductCategory(int categoryId)
        {
            return _productCategoryDao.Get(categoryId);
        }

        public IEnumerable<Product> GetProductsForCategory(int categoryId)
        {
            // TODO ask about how the categories get Included in Products from here
            ProductCategory category = _productCategoryDao.Get(categoryId);
            return _productDao.GetBy(category);
        }

        public IEnumerable<Product> GetProductsForSuplier(int suplierId)
        {
            Supplier suplier = _supplierDao.Get(suplierId);
            return _productDao.GetBy(suplier);
        }

        public IEnumerable<Supplier> GetAllSupliers()
        {
            return _supplierDao.GetAll();
        }

        public IEnumerable<ProductCategory> GetAllCategories()
        {
            return _productCategoryDao.GetAll();
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _productDao.GetAll();
        }

        //public IEnumerable<BaseModel> GetAll<T>()
        //{
        //    if (typeof(T) == typeof(Supplier))
        //        return _supplierDao.GetAll();

        //    else if (typeof(T) == typeof(ProductCategory)) 
        //        return _productCategoryDao.GetAll();

        //    else if (typeof(T) == typeof(Product))
        //        return _productDao.GetAll();

        //    else 
        //        return Enumerable.Empty<BaseModel>();
        //}
    }
}
