using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Codecool.CodecoolShop.Core.Models;
using Codecool.CodecoolShop.Extensions;
using Codecool.CodecoolShop.Services;
using Microsoft.AspNetCore.Http;
using EFDataAccessLibrary.Models;

namespace Codecool.CodecoolShop.Controllers
{
    public class OrderConfirmation : Controller
    {
        private readonly IEmailService _emailService;

        public OrderConfirmation(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public IActionResult Index()
        {
            var order = HttpContext.Session.GetOrder();

            if (order.PaymentStatus == true)
            {
                HttpContext.Session.SaveOrder(new Order());
                _emailService.Send("sales@ccshop.com", order.User.EmailAddress, "Your order in CCShop is complete!", _emailService.GetHtml(order));
            }

            return View(order);
        }
    }
}
