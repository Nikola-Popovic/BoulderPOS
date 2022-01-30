using System;
using BoulderPOS.API.Models;

namespace BoulderPOS.API.IntegrationsTests.DataSeed
{
    public static class PaymentSeeder
    {
        public static BillProduct WalkinBillProduct => new BillProduct()
        {
            Id = 1,
            ProductId = ProductSeeder.Product1Food.Id,
            SellingPrice = ProductSeeder.Product1Food.Price,
            ProcessedDateTime = DateTime.Now.AddDays(-3),
            UpdatedDateTime = DateTime.Now.AddDays(-3),
            IsRefunded = false
        };

        public static BillProduct Customer1BillProduct => new BillProduct()
        {
            Id = 2,
            CustomerId = CustomerSeeder.Customer1.Id,
            ProductId = ProductSeeder.Product2Equipment.Id,
            SellingPrice = ProductSeeder.Product2Equipment.Price,
            ProcessedDateTime = DateTime.Now.AddDays(-1),
            UpdatedDateTime = DateTime.Now.AddDays(-1),
            IsRefunded = false
        };

    }
}
