using System.ComponentModel.DataAnnotations;

namespace EFDataAccessLibrary.Models
{
    public class User 
    {
        [Display(Name = "First Name")]
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string LastName { get; set; }

        [Display(Name = "Email Address")]
        [StringLength(100, MinimumLength = 3)]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        [Required]
        public string EmailAddress { get; set; }

        [Display(Name = "Phone Number")]
        [StringLength(60, MinimumLength = 3)]
        public string PhoneNumber { get; set; }

        [Display(Name = "Billing Address")]
        [Required]
        public Address BillingAddress { get; set; }
        [Display(Name = "Shipping Address")]
        public Address ShippingAddress { get; set; }
    }
}
