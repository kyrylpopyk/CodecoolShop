using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Codecool.CodecoolShop.Core.Models
{
    public class Supplier : BaseModel
    {
        public List<Product> Products { get; set; }

        public override string ToString()
        {
            return new string($"Id: {Id} Name: {Name} Description: {Description}");
        }
    }
}
