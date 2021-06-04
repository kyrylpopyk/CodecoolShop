using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Codecool.CodecoolShop.Core.Models
{
    public class User
    {
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Billing Address")]
        public Address BillingAddress { get; set; }
        [Display(Name = "Shipping Address")]
        public Address ShippingAddress { get; set; }
    }
}
