using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Codecool.CodecoolShop.Core.Models
{
    public class Address
    {
        [Required]
        public string Country { get; set; }

        [StringLength(80, MinimumLength = 2)]
        [Required]
        public string City { get; set; }

        [Display(Name = "ZIP Code")]
        [StringLength(20, MinimumLength = 3)]
        [Required]
        public string ZipCode { get; set; }

        [Display(Name = "Street and home/apartment number")]
        [StringLength(100, MinimumLength = 3)]
        [Required]
        public string AddressString { get; set; }
    }
}
