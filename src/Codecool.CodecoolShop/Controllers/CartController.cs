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
            var order = GetOrderFromSession();
            SaveOrderInSession(order);

            return View(order);
        }

        public IActionResult AddProduct(int id, int quantity = 1) // the default quantity of products to add is 1
        {
            var order = GetOrderFromSession();

            AddProductToOrder(id, quantity, order);

            SaveOrderInSession(order);

            return Redirect("/Product");
        }

        public IActionResult Edit(Order orderEdit)
        {
            var order = GetOrderFromSession();
            UpdateQuantities(order, orderEdit);
            SaveOrderInSession(order);

            return RedirectToAction("Index", "Cart");
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            var order = GetOrderFromSession();
            var user = order.User ?? new User();

            return View(user);
        }

        [HttpPost]
        public IActionResult Checkout(User user)
        {
            if (ModelState.IsValid)
            {
                var order = GetOrderFromSession();
                order.User = user;
                SaveOrderInSession(order);
                return RedirectToAction("Index", "Cart");
            }

            return View(user);
        }


        private Order GetOrderFromSession()
        {
            var order = HttpContext.Session.Get<Order>("ShoppingCart");
            return order ?? new Order();
        }

        private void SaveOrderInSession(Order order)
        {
            HttpContext.Session.Set<Order>("ShoppingCart", order);

            var productsCount = order.Items.Sum(item => item.Quantity);

            HttpContext.Session.SetInt32("CartItemsCount", productsCount);
        }
        private void AddProductToOrder(int id, int quantity, Order order)
        {
            var lineItem = order.Items.FirstOrDefault(i => i.Product.Id == id); //TODO: Cart: should extract business logic like this to CodeCoolShop.Core?

            if (lineItem.IsNull()) //TODO: Cart: is this extension use reasonable?
            {
                order.Items.Add(new LineItem { Product = ProductService.GetProduct(id), Quantity = quantity });
            }
            else
            {
                lineItem.Quantity += quantity;
            }
        }

        private void UpdateQuantities(Order order, Order orderEdit)
        {
            var itemsToDelete = new List<LineItem>();

            foreach (var itemEdit in orderEdit.Items) // TODO: Cart: how to do this with Linq?
            {
                var itemToEdit = order.Items[orderEdit.Items.IndexOf(itemEdit)]; // TODO: Cart: Is using list indexes like this ok?
                itemToEdit.Quantity = itemEdit.Quantity;
                if (itemToEdit.Quantity < 1) itemsToDelete.Add(itemToEdit);
            }

            itemsToDelete.ForEach(i => order.Items.Remove(i));
        }
    }
}
