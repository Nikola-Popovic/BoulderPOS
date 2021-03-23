using System;
using System.Collections.Generic;
using System.Text;
using BoulderPOS.API.Models;

namespace BoulderPOS.API.IntegrationsTests.DataSeed
{
    public static class ProductSeeder
    {
        public static ProductCategory FoodCategory => new ProductCategory() {Id = 1, Name = "Food"};
        public static ProductCategory EquipmentCategory => new ProductCategory() {Id = 2, Name = "Equipment"};
        public static ProductCategory CategoryToDelete => new ProductCategory() {Id = 3, Name = "ToDelete"};

        public static Product Product1Food => new Product()
            {CategoryId = FoodCategory.Id, Id = 1, Name = "Grilled Cheese", Price = new decimal(4.50)};

        public static Product Product2Equipment => new Product()
            { CategoryId = EquipmentCategory.Id, Id = 2, Name = "Sparca SP-10", Price = new decimal(164.50) };

        public static Product ProductToRemove => new Product()
            { CategoryId = FoodCategory.Id, Id = 3, Name = "Grilled Cheese Duplicate", Price = new decimal(7.50) };

        public static Product ProductWithoutInventory => new Product()
            { CategoryId = FoodCategory.Id, Id = 4, Name = "Fish Sticks", Price = new decimal(4.50) };

        public static ProductInventory Product1Inventory => new ProductInventory()
            {InStoreQuantity = 5, OrderedQuantity = 10, SuretyQuantity = 9, ProductId = Product1Food.Id};

        public static ProductInventory Product2Inventory => new ProductInventory()
            {InStoreQuantity = 3, OrderedQuantity = 0, SuretyQuantity = 1, ProductId = Product2Equipment.Id };

        public static ProductInventory ProductToDeleteInventory => new ProductInventory()
            { InStoreQuantity = 3, OrderedQuantity = 0, SuretyQuantity = 1, ProductId = ProductToRemove.Id };
    }
}
