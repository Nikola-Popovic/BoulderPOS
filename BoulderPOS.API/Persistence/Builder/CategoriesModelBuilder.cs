using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoulderPOS.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BoulderPOS.API.Persistence.Builder
{
    public static class CategoriesModelBuilder
    {
        public static ModelBuilder AddInitialDataSeed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductCategory>().HasData(new ProductCategory
            {
                IconName = "fas fa-ticket-alt",
                IsEntries = true,
                Name = "Entries",
                Id = -1,
                Order = 1
            });

            modelBuilder.Entity<ProductCategory>().HasData(new ProductCategory
            {
                IconName = "fas fa-user-clock",
                IsSubscription = true,
                Name = "Subscription",
                Id = -2,
                Order = 2
            });


            return modelBuilder;
        }
    }
}
