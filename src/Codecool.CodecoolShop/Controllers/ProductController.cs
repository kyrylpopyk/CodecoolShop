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
                new ProductCategoryDaoMemory(_db));
        }

        public IActionResult Index()
        {
            var products = ProductService.GetProductsForCategory(1);
            return View(products.ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
