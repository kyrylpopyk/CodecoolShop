using System.Collections.Generic;
using Codecool.CodecoolShop.Daos;
using System.Linq;
using EFDataAccessLibrary.Models;

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

        public Product GetProduct(int id)
        {
            return _productDao.Get(id);
        }

        public IEnumerable<Product> GetProductBySeveralTypes(string supplierId, string categoryId)
        {
            if (supplierId != "" && categoryId != "")
            {
                return _productDao.GetBySupplierAndCategory(
                    _supplierDao.Get(int.Parse(supplierId)), 
                    _productCategoryDao.Get(int.Parse(categoryId)));
            }
            else if ( supplierId != "")
            {
                return GetProductsForSuplier(int.Parse(supplierId));
            }
            else if ( categoryId != "")
            {
                return GetProductsForCategory(int.Parse(categoryId));
            }
            else
            {
                return GetAllProducts();
            }
        }
    }
}
