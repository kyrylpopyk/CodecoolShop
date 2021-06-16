using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
            var userIsValid = ValidateUserData(user);

            if (userIsValid)
            {
                return View();
            }

            TempData["Missing details"] = true;

            return RedirectToAction("Checkout", "Cart");
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

        private static bool ValidateUserData(User user)
        {
            var validationContext = new ValidationContext(user);
            var validationResults = new List<ValidationResult>();
            bool userDataIsValid = Validator.TryValidateObject(user, validationContext, validationResults, true);
            return userDataIsValid;
        }
    }
}
