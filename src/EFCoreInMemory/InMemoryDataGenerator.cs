using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Codecool.CodecoolShop.Core.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace EFCoreInMemory
{
    public class InMemoryDataGenerator
    {
        public static void Init(InMemoryDb db)
        {
            if (db.Products.Any())
            {
                return;
            }

            Supplier amazon = new Supplier { Name = "Amazon", Description = "Digital content and services" };
            db.Add(amazon);
            Supplier lenovo = new Supplier { Name = "Lenovo", Description = "Computers" };
            db.Add(lenovo);
            ProductCategory tablet = new ProductCategory { Name = "Tablet", Department = "Hardware", Description = "A tablet computer, commonly shortened to tablet, is a thin, flat mobile computer with a touchscreen display." };
            db.Add(tablet);
            db.Add(new Product { Name = "Amazon Fire", DefaultPrice = 49.9m, Currency = "USD", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = tablet, Supplier = amazon });
            db.Add(new Product { Name = "Lenovo IdeaPad Miix 700", DefaultPrice = 479.0m, Currency = "USD", Description = "Keyboard cover is included. Fanless Core m5 processor. Full-size USB ports. Adjustable kickstand.", ProductCategory = tablet, Supplier = lenovo });
            db.Add(new Product { Name = "Amazon Fire HD 8", DefaultPrice = 89.0m, Currency = "USD", Description = "Amazon's latest Fire HD 8 tablet is a great value for media consumption.", ProductCategory = tablet, Supplier = amazon });

            db.SaveChanges();
        }
    }
}
