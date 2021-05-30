using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codecool.CodecoolShop.Models
{
    public class User
    {
        public int Id { get; set; }
        public List<Address> Addresses { get; } = new List<Address>();
    }
}
