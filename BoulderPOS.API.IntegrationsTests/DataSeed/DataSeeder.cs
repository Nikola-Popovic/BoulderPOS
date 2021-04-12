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
            AddEntries(dbContext);
            AddSubscription(dbContext);
            AddCategories(dbContext);
            AddProducts(dbContext);
            AddPayments(dbContext);
        }

        private static void AddSubscription(ApplicationDbContext dbContext)
        {
            dbContext.CustomerSubscriptions.Add(CustomerSeeder.ValidSubscription);
            dbContext.CustomerSubscriptions.Add(CustomerSeeder.InvalidSubscription);
            dbContext.SaveChanges();
        }

        private static void AddCustomers(ApplicationDbContext dbContext)
        {
            dbContext.Customers.Add(CustomerSeeder.Customer1);
            dbContext.Customers.Add(CustomerSeeder.Customer2);
            dbContext.Customers.Add(CustomerSeeder.CustomerWithNoEntries);
            dbContext.Customers.Add(CustomerSeeder.CustomerToDelete);
            dbContext.Customers.Add(CustomerSeeder.CustomerWithValidSubscription);
            dbContext.Customers.Add(CustomerSeeder.CustomerWithInvalidSubscription);
            dbContext.SaveChanges();
        }
        private static void AddEntries(ApplicationDbContext dbContext)
        {
            dbContext.CustomerEntries.Add(CustomerSeeder.Customer1Entries);
            dbContext.CustomerEntries.Add(CustomerSeeder.Customer2Entries);
            dbContext.CustomerEntries.Add(CustomerSeeder.CustomerWithNoEntriesEntries);
            dbContext.CustomerEntries.Add(CustomerSeeder.CustomerWithValidSubscriptionEntries);
            dbContext.SaveChanges();
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
            dbContext.Products.Add(ProductSeeder.ProductWithoutInventory);
            dbContext.Products.Add(ProductSeeder.EntriesProduct);
            dbContext.Products.Add(ProductSeeder.SubscriptionProduct);
            dbContext.Products.Add(ProductSeeder.UnavailableProduct);
            AddProductInventory(dbContext);
        }

        private static void AddProductInventory(ApplicationDbContext dbContext)
        {
            dbContext.ProductInventory.Add(ProductSeeder.Product1Inventory);
            dbContext.ProductInventory.Add(ProductSeeder.Product2Inventory);
            dbContext.ProductInventory.Add(ProductSeeder.ProductToDeleteInventory);
            dbContext.SaveChanges();
        }

        private static void AddPayments(ApplicationDbContext dbContext)
        {
            dbContext.ProductPayments.Add(PaymentSeeder.WalkinFoodPayment);
            dbContext.ProductPayments.Add(PaymentSeeder.Customer1Payment);
            dbContext.SaveChanges();
        }

    }
}
