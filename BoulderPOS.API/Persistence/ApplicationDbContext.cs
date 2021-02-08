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
    }
}
