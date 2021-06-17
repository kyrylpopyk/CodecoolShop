using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Codecool.CodecoolShop.Daos.Implementations;
using Microsoft.AspNetCore.Http;
using Codecool.CodecoolShop.Extensions;
using Codecool.CodecoolShop.Services;
using CountryData.Standard;
using EFCoreInMemory;
using EFDataAccessLibrary.DataAccess;
using Codecool.CodecoolShop.Core.Models;

namespace Codecool.CodecoolShop.Controllers
{
    public class CartController : Controller
    {
        public ProductService ProductService { get; }
        public CountryHelper _countryHelper;
        public CartController(CCShopContext db)
        {
            ProductService = new ProductService(
                new ProductDaoEF(db),
                new ProductCategoryDaoEF(db),
                new SupplierDaoEF(db));
            _countryHelper = new CountryHelper();
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

            ViewBag.MissingDetails = TempData["Missing details"] ?? false;
            ViewBag.Countries = _countryHelper.GetCountries();

            return View(user);
        }

        [HttpPost]
        public IActionResult Checkout(User user)
        {
            if (ModelState.IsValid)
            {
                var order = GetOrderFromSession();
                order.User = user;
                user.ShippingAddress = user.BillingAddress;
                order.ShippingAddress = user.ShippingAddress;
                SaveOrderInSession(order);
                return RedirectToAction("Index", "Cart");
            }

            return View(user);
        }


        private Order GetOrderFromSession()
        {
            return HttpContext.Session.GetOrder();
        }

        private void SaveOrderInSession(Order order)
        {
            HttpContext.Session.SaveOrder(order);
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
