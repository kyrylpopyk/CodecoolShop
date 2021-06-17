using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codecool.CodecoolShop.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCoreInMemory
{
    public class InMemoryDb : DbContext
    {
        public InMemoryDb(DbContextOptions<InMemoryDb> options): base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
    }
}
