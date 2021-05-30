using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Daos.Implementations;
using Codecool.CodecoolShop.InMemoreEF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Codecool.CodecoolShop.Models;
using Codecool.CodecoolShop.Services;
using Microsoft.EntityFrameworkCore;

namespace Codecool.CodecoolShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly CCShopInMemoryDB _db;
        public ProductService ProductService { get; set; }

        public ProductController(ILogger<ProductController> logger, CCShopInMemoryDB db)
        {
            _logger = logger;
            _db = db;
            ProductService = new ProductService(
                ProductDaoMemory.GetInstance(),
                ProductCategoryDaoMemory.GetInstance());
        }

        public IActionResult Index()
        {
            var products = ProductService.GetProductsForCategory(1);

            var usr1 = new User();
            var usr2 = new User();
            var adrs1 = new Address();
            var adrs2 = new Address();
            usr1.Addresses.Add(adrs1);

            usr2.Addresses.Add(adrs1);
            usr2.Addresses.Add(adrs2);

            adrs1.Users.Add(usr1);
            adrs1.Users.Add(usr2);

            adrs2.Users.Add(usr2);


            _db.Users.AddRange(usr1, usr2);


            _db.SaveChanges();

            var usersFromMemory = _db.Users.ToList();//.Include(u => u.Addresses);

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
