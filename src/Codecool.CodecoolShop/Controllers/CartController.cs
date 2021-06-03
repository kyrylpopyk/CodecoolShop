using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Codecool.CodecoolShop.Core.Models;
using Codecool.CodecoolShop.Daos.Implementations;
using Microsoft.AspNetCore.Http;
using Codecool.CodecoolShop.Extensions;
using Codecool.CodecoolShop.Services;
using EFCoreInMemory;

namespace Codecool.CodecoolShop.Controllers
{
    public class CartController : Controller
    {
        public ProductService ProductService { get; }
        public CartController(InMemoryDb db)
        {
            ProductService = new ProductService(
                new ProductDaoMemory(db),
                new ProductCategoryDaoMemory(db));
        }
        public IActionResult Index()
        {
            Order order = GetOrderFromSession();
            SaveOrderInSession(order);

            return View(order);
        }

        public IActionResult AddProduct(int id, int quantity = 1) // the default quantity of products to add is 1
        {
            Order order = GetOrderFromSession();

            var lineItem = order.Items.FirstOrDefault(i => i.Product.Id == id); //TODO: should extract business logic like this to CodeCoolShop.Core?

            if (lineItem.IsNull()) //TODO: is this extension use reasonable?
            {
                order.Items.Add(new LineItem { Product = ProductService.GetProduct(id), Quantity = quantity});
            }
            else
            {
                lineItem.Quantity += quantity;
            }

            SaveOrderInSession(order);

            return Redirect("/Product");
        }

        private Order GetOrderFromSession()
        {
            Order order = HttpContext.Session.Get<Order>("ShoppingCart");
            return order ?? new Order();
        }

        private void SaveOrderInSession(Order order)
        {
            HttpContext.Session.Set<Order>("ShoppingCart", order);

            var productsCount = order.Items.Sum(item => item.Quantity);

            HttpContext.Session.SetInt32("CartItemsCount", productsCount);
        }
    }
}
