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

            AddCategories(dbContext);
            AddProducts(dbContext);
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

        private static void AddCategories(ApplicationDbContext dbContext)
        {
            dbContext.ProductCategories.Add(ProductSeeder.FoodCategory);
            dbContext.ProductCategories.Add(ProductSeeder.EquipmentCategory);
            dbContext.ProductCategories.Add(ProductSeeder.CategoryToDelete);
            dbContext.SaveChanges();
        }

        private static void AddProducts(ApplicationDbContext dbContext)
        {
            dbContext.Products.Add(ProductSeeder.Product1Food);
            dbContext.Products.Add(ProductSeeder.Product2Equipment);
            dbContext.Products.Add(ProductSeeder.ProductToRemove);
            dbContext.SaveChanges();
            AddProductInventory(dbContext);
        }

        private static void AddProductInventory(ApplicationDbContext dbContext)
        {
            dbContext.ProductInventory.Add(ProductSeeder.Product1Inventory);
            dbContext.ProductInventory.Add(ProductSeeder.Product2Inventory);
            dbContext.ProductInventory.Add(ProductSeeder.ProductToDeleteInventory);
            dbContext.SaveChanges();
        }

    }
}
