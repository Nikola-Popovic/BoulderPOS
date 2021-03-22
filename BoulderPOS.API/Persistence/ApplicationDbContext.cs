using System.Transactions;
using BoulderPOS.API.Models;
using BoulderPOS.API.Persistence.Builder;
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

            modelBuilder.ConfigureProductModelBuilder();
            modelBuilder.ConfigureCustomerModelBuilder();

            // modelBuilder.Entity<ProductInventory>().HasIndex(c => c.ProductId).IsUnique();
            modelBuilder.Entity<ProductInventory>().HasKey(c => c.ProductId);
            // modelBuilder.Entity<CustomerSubscription>().HasIndex(s => s.CustomerId).IsUnique();
            modelBuilder.Entity<CustomerSubscription>().HasKey(c => c.CustomerId);
            modelBuilder.Entity<CustomerSubscription>().HasOne(a => a.Customer).WithOne(a => a.Subscription)
                .HasForeignKey<CustomerSubscription>(b => b.CustomerId).IsRequired().OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CustomerEntries>().HasOne(a => a.Customer).WithOne(a => a.Entries)
                .HasForeignKey<CustomerEntries>(b => b.CustomerId);
            modelBuilder.Entity<ProductCategory>().HasMany(c => c.Products).WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId);
        }
    }
}
