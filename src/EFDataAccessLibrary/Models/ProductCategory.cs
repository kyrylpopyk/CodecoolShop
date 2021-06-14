using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFDataAccessLibrary.Models
{
    public class ProductCategory: BaseModel
    {
        public List<Product> Products { get; set; }

        public string Department { get; set; }
    }
}
