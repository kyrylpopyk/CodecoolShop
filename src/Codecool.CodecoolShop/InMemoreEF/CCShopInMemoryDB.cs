using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Codecool.CodecoolShop.Models;
using Microsoft.EntityFrameworkCore;

namespace Codecool.CodecoolShop.InMemoreEF
{
    public class CCShopInMemoryDB : DbContext
    {
        public CCShopInMemoryDB(DbContextOptions<CCShopInMemoryDB> options) : base(options)
        {
                
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<User>().HasKey(u => new {u.Id});
        //}
    }
}
