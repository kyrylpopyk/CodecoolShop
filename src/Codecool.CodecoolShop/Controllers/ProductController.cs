using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Daos.Implementations;
using Codecool.CodecoolShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Codecool.CodecoolShop.Core.Models;
using Codecool.CodecoolShop.Services;
using EFCoreInMemory;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;

namespace Codecool.CodecoolShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly InMemoryDb _db;
        public ProductService ProductService { get; set; }

        public ProductController(ILogger<ProductController> logger, InMemoryDb db)
        {
            _logger = logger;
            _db = db;
            ProductService = new ProductService(
                new ProductDaoMemory(_db),
                new ProductCategoryDaoMemory(_db),
                new SupplierDaoMemory(_db));
        }

        public IActionResult Index()
        {
            var supplierFilterId = Request.Query["supplierId"].ToString();
            var categoryFilterId = Request.Query["pCategoryId"].ToString();

            var products = ProductService.GetProductBySeveralTypes(supplierFilterId, categoryFilterId);
            var categories = ProductService.GetAllCategories();
            var suppliers = ProductService.GetAllSupliers();

            return View(new List<(List<Product>, List<ProductCategory>, List<Supplier>)>() 
            {
                (products.ToList(), categories.ToList(), suppliers.ToList())
            });
        }

        [HttpPost]
        public IActionResult ChangeFilter(string supplierId, string pCategoryId)
        {
            var routeValues = new RouteValueDictionary()
            {
                { "supplierId", supplierId },
                { "pCategoryId", pCategoryId}
            };
            return RedirectToAction("Index", "Product", routeValues);
        }

    }
}
