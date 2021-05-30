using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codecool.CodecoolShop.Models
{
    public class Address
    {
        public int Id { get; set; }
        public List<User> Users { get; } = new List<User>();
    }
}
