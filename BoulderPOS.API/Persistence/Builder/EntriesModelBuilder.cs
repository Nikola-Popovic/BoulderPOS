using BoulderPOS.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BoulderPOS.API.Persistence.Builder
{
    public static class EntriesModelBuilder
    {
        public static ModelBuilder ConfigureEntriesModelBuilder(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerEntries>().HasOne(a => a.Customer).WithOne(a => a.Entries)
                .HasForeignKey<CustomerEntries>(b => b.CustomerId);
            modelBuilder.Entity<ProductCategory>().HasMany(c => c.Products).WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId);
            return modelBuilder;
        }
    }
}
