using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using WA.Pizza.Core.Models;
using WA.Pizza.Core.Models.Items;

namespace WA.Pizza.Infrastructure
{
    public class PizzaContext : DbContext
    {
        public DbSet<CatalogItem> CatalogItems { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public PizzaContext(DbContextOptions<PizzaContext> options) : base(options) { }

        public async Task SaveChangesAsync(CancellationToken token = default)
        {
            await SaveChangesAsync(token);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var catalogItem = modelBuilder.Entity<CatalogItem>();
            catalogItem.HasKey(m => m.Id);
            catalogItem.Property(m => m.Name).IsRequired().HasMaxLength(64);
            catalogItem.Property(m => m.Description).IsRequired().HasMaxLength(256);
            catalogItem.Property(m => m.Price).IsRequired();

            var order = modelBuilder.Entity<Order>();
            order.HasKey(m => m.Id);

            var orderItem = modelBuilder.Entity<OrderItem>();
            orderItem.HasKey(m => m.Id);
            orderItem.HasOne(m => m.Order).WithMany(m => m.Items);
            orderItem.Property(m => m.Name).IsRequired().HasMaxLength(64);
            orderItem.Property(m => m.Price).IsRequired();
            orderItem.Property(m => m.Quantity).IsRequired();

            var basket = modelBuilder.Entity<Basket>();
            basket.HasKey(m => m.Id);

            var basketItem = modelBuilder.Entity<BasketItem>();
            basketItem.HasKey(m => m.Id);
            basketItem.HasOne(m => m.Basket).WithMany(m => m.Items);
            basketItem.Property(m => m.Name).IsRequired().HasMaxLength(64);
            basketItem.Property(m => m.Price).IsRequired();
            basketItem.Property(m => m.Quantity).IsRequired();
        }
    }
}
