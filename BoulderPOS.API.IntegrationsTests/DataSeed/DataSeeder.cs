using System;
using BoulderPOS.API.Models;
using BoulderPOS.API.Persistence;

namespace BoulderPOS.API.IntegrationsTests.DataSeed
{
    public static class DataSeeder
    {
        public static void PopulateTestData(ApplicationDbContext dbContext)
        {
            AddEntries(dbContext);
            AddCustomers(dbContext);
            dbContext.SaveChanges();
        }
        
        private static void AddCustomers(ApplicationDbContext dbContext)
        {
            dbContext.Customers.Add(CustomerSeeder.Customer1);
            dbContext.Customers.Add(CustomerSeeder.Customer2);
            dbContext.Customers.Add(CustomerSeeder.CustomerToDelete);
        }


        private static void AddEntries(ApplicationDbContext dbContext)
        {
            dbContext.CustomerEntries.Add(CustomerSeeder.Customer1Entries);
            dbContext.CustomerEntries.Add(CustomerSeeder.Customer2Entries);
        }

    }
}
