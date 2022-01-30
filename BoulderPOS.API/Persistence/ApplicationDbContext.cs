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
        public DbSet<BillProduct> BillProducts { get; set; }
        public DbSet<Bill> Bills { get; set; }
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
            modelBuilder.Entity<BillProduct>()
                .HasOne(a => a.Customer)
                .WithMany(c => c.BillProducts)
                .IsRequired(false)
                .HasForeignKey(p => p.CustomerId);

            modelBuilder.Entity<Bill>()
                .HasOne(a => a.Customer)
                .WithMany(c => c.Bills)
                .IsRequired(false)
                .HasForeignKey(p => p.CustomerId);
        }
    }
}
