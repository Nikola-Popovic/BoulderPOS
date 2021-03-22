using BoulderPOS.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BoulderPOS.API.Persistence.Builder
{
    public static class ProductModelBuilder
    {
        public static ModelBuilder ConfigureProductModelBuilder(this ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Product>().HasOne(a => a.Category).WithMany()
                .HasForeignKey(entry => entry.CategoryId);

            modelBuilder.Entity<Product>().HasOne(a => a.Inventory)
                .WithOne(inv => inv.Product)
                .HasForeignKey<ProductInventory>(inv => inv.ProductId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            modelBuilder.Entity<Product>().HasMany(a => a.Orders).WithOne(orders => orders.Product).HasForeignKey(order => order.ProductId).IsRequired(true);

            return modelBuilder;
        }
    }
}
