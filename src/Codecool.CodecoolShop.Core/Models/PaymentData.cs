using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Codecool.CodecoolShop.Core.Models
{
    public class PaymentData
    {
        [Required]
        [Display(Name = "Card Holder Name")]
        public string CardHolder { get; set; }

        [Required]
        [DataType(DataType.CreditCard)]
        public int CardNumber { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ExpiryDate { get; set; }

        [Required]
        [Display(Name = "CVV")]
        public int Cvv { get; set; }
    }
}
