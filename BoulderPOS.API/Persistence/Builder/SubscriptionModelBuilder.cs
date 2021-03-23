using BoulderPOS.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BoulderPOS.API.Persistence.Builder
{
    public static class SubscriptionModelBuilder
    {
        public static ModelBuilder ConfigureSubscriptionBuilder(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerSubscription>().HasKey(c => c.CustomerId);
            modelBuilder.Entity<CustomerSubscription>().HasOne(a => a.Customer)
                .WithOne(a => a.Subscription)
                .HasForeignKey<CustomerSubscription>(b => b.CustomerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            return modelBuilder;
        }
    }
}
