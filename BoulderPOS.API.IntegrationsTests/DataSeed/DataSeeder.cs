using System;
using BoulderPOS.API.Models;
using BoulderPOS.API.Persistence;

namespace BoulderPOS.API.IntegrationsTests.DataSeed
{
    public static class DataSeeder
    {
        public static void PopulateTestData(ApplicationDbContext dbContext)
        {
            AddCustomers(dbContext);
            dbContext.SaveChanges();
        }

        
        private static void AddCustomers(ApplicationDbContext dbContext)
        {
            dbContext.Customers.Add(CustomerSeeder.Customer1);
            dbContext.Customers.Add(CustomerSeeder.Customer2);
            dbContext.Customers.Add(CustomerSeeder.CustomerToDelete);
        }
        
    }
}
