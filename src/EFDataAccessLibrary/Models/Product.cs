using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFDataAccessLibrary.Models
{
    public class Product : BaseModel
    {
        [Required]
        [Column(TypeName = "nvarchar(10)")]
        public string Currency { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")] 
        public decimal DefaultPrice { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public ProductCategory ProductCategory { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public Supplier Supplier { get; set; }

        public void SetProductCategory(ProductCategory productCategory)
        {
            ProductCategory = productCategory;
            ProductCategory.Products.Add(this);
        }
    }
}
