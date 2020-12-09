using Microsoft.EntityFrameworkCore;
using ProductShop.API.Data.Domains;
using ProductShop.API.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductShop.API.Data
{
    public class ProductShopContext : DbContext
    {
        public ProductShopContext(DbContextOptions<ProductShopContext> opt) : base(opt)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
