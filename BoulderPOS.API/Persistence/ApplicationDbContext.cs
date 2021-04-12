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

            modelBuilder.AddInitialDataSeed();
            modelBuilder.ConfigureProductModelBuilder();
            modelBuilder.ConfigureCustomerModelBuilder();
            modelBuilder.ConfigureSubscriptionModelBuilder();
            modelBuilder.ConfigureEntriesModelBuilder();

            modelBuilder.Entity<ProductInventory>().HasKey(c => c.ProductId);
            modelBuilder.Entity<ProductPayment>()
                .HasOne(a => a.Customer)
                .WithMany(c => c.Orders)
                .IsRequired(false)
                .HasForeignKey(p => p.CustomerId);
        }
    }
}
