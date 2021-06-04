﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Codecool.CodecoolShop.Core.Models
{
    public class LineItem : BaseModel
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price => Product.DefaultPrice; //TODO: is this gonna work? :D
    }
}
