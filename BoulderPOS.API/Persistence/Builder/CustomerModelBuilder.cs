using BoulderPOS.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BoulderPOS.API.Persistence.Builder
{
    public static class CustomerModelBuilder
    {
        public static ModelBuilder ConfigureCustomerModelBuilder(this ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Customer>()
                .HasOne(a => a.Entries)
                .WithOne(entry => entry.Customer)
                .HasForeignKey<CustomerEntries>(entry => entry.CustomerId);

            return modelBuilder;
        }
    }
}
