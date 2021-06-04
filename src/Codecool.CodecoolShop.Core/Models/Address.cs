using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Codecool.CodecoolShop.Core.Models
{
    public class Address
    {
        public string Country { get; set; }
        public string City { get; set; }
        [Display(Name = "ZIP Code")]
        public string ZipCode { get; set; }
        [Display(Name = "Street and home/apartment number")]
        public string AddressString { get; set; }
    }
}
