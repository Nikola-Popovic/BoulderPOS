using BoulderPOS.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BoulderPOS.API.Persistence.Builder
{
    public static class ProductModelBuilder
    {
        public static ModelBuilder ConfigureProductModelBuilder(this ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Product>()
                .HasOne(a => a.Category)
                .WithMany()
                .HasForeignKey(entry => entry.CategoryId);

            modelBuilder.Entity<Product>()
                .HasMany(a => a.Orders)
                .WithOne(orders => orders.Product)
                .HasForeignKey(order => order.ProductId)
                .IsRequired();

            modelBuilder.Entity<Product>().HasOne(a => a.Inventory)
                .WithOne(b => b.Product)
                .HasForeignKey<ProductInventory>(inv => inv.ProductId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            return modelBuilder;
        }
    }
}
