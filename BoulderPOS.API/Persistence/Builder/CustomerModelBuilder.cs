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

            modelBuilder.Entity<Customer>()
                .HasMany(a => a.Orders)
                .WithOne(orders => orders.Customer)
                .HasForeignKey(order => order.CustomerId)
                .IsRequired(false);

            /*
            modelBuilder.Entity<Customer>()
                .HasOne(a => a.Subscription)
                .WithOne(sus => sus.Customer)
                .HasForeignKey<CustomerSubscription>(sus => sus.CustomerId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
            */
            return modelBuilder;
        }
    }
}
