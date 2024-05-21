using Microsoft.EntityFrameworkCore;
using Rocket.Core.Entities;
using Rocket.Core.Entities.Identity.OrderAgreggate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Rocket.Repository.Data
{
    public class RocketStoreDbContext:DbContext
    {
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //=>
        //    optionsBuilder.UseSqlServer("");

        public RocketStoreDbContext(DbContextOptions<RocketStoreDbContext> options):base(options)            
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> Items { get; set; }
        public DbSet<DeliveryMethod> DeliveryMethods { get; set;}
      
    }
}
