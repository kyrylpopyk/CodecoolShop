using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Codecool.CodecoolShop.Core.Models;

namespace Codecool.CodecoolShop.Services
{
    public class PaymentService
    {
        private Random _rng = new Random();

        public bool ProcessPayment(PaymentData data)
        {
            return _rng.Next(10) < 7 ? true : false;
        }
    }
}
