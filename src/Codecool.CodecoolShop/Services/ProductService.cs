using System.Collections.Generic;
using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Core.Models;

namespace Codecool.CodecoolShop.Services
{
    public class ProductService
    {
        private readonly IProductDao _productDao;
        private readonly IProductCategoryDao _productCategoryDao;

        public ProductService(IProductDao productDao, IProductCategoryDao productCategoryDao)
        {
            _productDao = productDao;
            _productCategoryDao = productCategoryDao;
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
    }
}
