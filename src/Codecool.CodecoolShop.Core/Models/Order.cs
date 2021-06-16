﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codecool.CodecoolShop.Core.Models
{
    public class Order : BaseModel
    {
        public List<LineItem> Items { get; } = new List<LineItem>();
        public User User { get; set; }
        public Address BillingAddress { get; set; }
        public Address ShippingAddress { get; set; }
        public bool PaymentStatus { get; set; }
    }
}
