using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Codecool.CodecoolShop.Core.Models;
using Codecool.CodecoolShop.Extensions;
using Microsoft.AspNetCore.Http;

namespace Codecool.CodecoolShop.Controllers
{
    public class PaymentController : Controller
    {
        public IActionResult Index()
        {
            var user = GetOrderFromSession().User ?? new User();
            var validationContext = new ValidationContext(user);
            var validationResults = new List<ValidationResult>();
            bool userDataIsValid = Validator.TryValidateObject(user, validationContext, validationResults, true);

            if (userDataIsValid)
            {
                return View();
            }

            TempData["Missing details"] = true;
            return RedirectToAction("Checkout", "Cart"); //TODO add viewbag info about missing data?
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
    }
}
