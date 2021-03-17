using System.Transactions;
using BoulderPOS.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BoulderPOS.API.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerEntries> CustomerEntries { get; set; }
        public DbSet<CustomerSubscription> CustomerSubscriptions { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductPayment> ProductPayments { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductInventory> ProductInventory { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>(cus =>
            {
                cus.HasOne(a => a.Entries).WithOne(entry => entry.Customer)
                    .HasForeignKey<CustomerEntries>(entry => entry.CustomerId);

                cus.HasOne(a => a.Subscription).WithOne(sus => sus.Customer)
                    .HasForeignKey<CustomerSubscription>(sus => sus.CustomerId).IsRequired(false);

                cus.HasMany(a => a.Orders).WithOne(orders => orders.Customer).HasForeignKey(order => order.CustomerId).IsRequired(false);
            });

            modelBuilder.Entity<Product>(cus =>
            {
                cus.HasOne(a => a.Category).WithMany()
                    .HasForeignKey(entry => entry.CategoryId);

                cus.HasOne(a => a.Inventory).WithOne(inv => inv.Product)
                    .HasForeignKey<ProductInventory>(inv => inv.ProductId).IsRequired(false);

                cus.HasMany(a => a.Orders).WithOne(orders => orders.Product).HasForeignKey(order => order.ProductId).IsRequired(true);
            });

            modelBuilder.Entity<CustomerEntries>().HasOne(a => a.Customer).WithOne(a => a.Entries)
                .HasForeignKey<CustomerEntries>(b => b.CustomerId);
            modelBuilder.Entity<CustomerSubscription>().HasOne(a => a.Customer).WithOne(a => a.Subscription)
                .HasForeignKey<CustomerSubscription>(b => b.CustomerId);
            modelBuilder.Entity<ProductInventory>().HasOne(i => i.Product).WithOne(p => p.Inventory)
                .HasForeignKey<ProductInventory>(i => i.ProductId);
            modelBuilder.Entity<ProductCategory>().HasMany(c => c.Products).WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId);
        }
    }
}
