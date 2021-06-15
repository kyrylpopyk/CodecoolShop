using System;
using System.Collections.Generic;
using System.Text;

namespace EFDataAccessLibrary.Models
{
    public class LineItem : BaseModel
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price => Product?.DefaultPrice ?? 0; //TODO: is this gonna work? :D
    }
}
