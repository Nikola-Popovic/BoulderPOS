using BoulderPOS.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BoulderPOS.API.Persistence.Builder
{
    public static class CustomerModelBuilder
    {
        public static ModelBuilder ConfigureCustomerModelBuilder(this ModelBuilder modelBuilder)
        {
            // Todo : Make email unique, add error for email already exists and display
            // modelBuilder.Entity<Customer>().HasIndex(c => c.Email).IsUnique(); 

            modelBuilder.Entity<Customer>()
                .HasOne(a => a.Entries)
                .WithOne(entry => entry.Customer)
                .HasForeignKey<CustomerEntries>(entry => entry.CustomerId);

            return modelBuilder;
        }
    }
}
