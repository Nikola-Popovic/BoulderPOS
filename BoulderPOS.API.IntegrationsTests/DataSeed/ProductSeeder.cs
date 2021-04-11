using BoulderPOS.API.Models;

namespace BoulderPOS.API.IntegrationsTests.DataSeed
{
    public static class ProductSeeder
    {
        public static ProductCategory FoodCategory => new ProductCategory() {Id = 1, Name = "Food", Order = 3};
        public static ProductCategory EquipmentCategory => new ProductCategory() {Id = 2, Name = "Equipment", Order = 0};
        public static ProductCategory CategoryToDelete => new ProductCategory() {Id = 3, Name = "ToDelete", Order = 4};

        public static int EntriesCategoryId = -1;

        public static ProductCategory EntriesCategory => new ProductCategory()
            {Id = EntriesCategoryId, IsEntries = true, Name = "Entries", Order = 1, IconName = "fas fa-ticket-alt"};

        public static int SubscriptionCategoryId = -2;

        public static ProductCategory SubscriptionCategory => new ProductCategory()
            { Id = SubscriptionCategoryId, IsSubscription = true, Name = "Subscription", Order = 2, IconName = "fas fa-user-clock" };

        public static Product Product1Food => new Product()
            {CategoryId = FoodCategory.Id, Id = 1, Name = "Grilled Cheese", Price = new decimal(4.50)};

        public static Product Product2Equipment => new Product()
            { CategoryId = EquipmentCategory.Id, Id = 2, Name = "Sparca SP-10", Price = new decimal(164.50) };

        public static Product ProductToRemove => new Product()
            { CategoryId = FoodCategory.Id, Id = 3, Name = "Grilled Cheese Duplicate", Price = new decimal(7.50) };

        public static Product ProductWithoutInventory => new Product()
            { CategoryId = FoodCategory.Id, Id = 4, Name = "Fish Sticks", Price = new decimal(4.50) };

        public static Product EntriesProduct => new Product()
        {
            Id = 5,
            CategoryId = EntriesCategoryId,
            Quantity = 10,
            Name = "Entries x10",
            Price = new decimal(120.0)
        };

        public static Product SubscriptionProduct => new Product()
        {
            Id = 6,
            CategoryId = SubscriptionCategoryId,
            DurationInMonths = 1,
            Name = "Subscription 1 month",
            Price = new decimal(80.0)
        };

        public static ProductInventory Product1Inventory => new ProductInventory()
            {InStoreQuantity = 5, OrderedQuantity = 10, SuretyQuantity = 9, ProductId = Product1Food.Id};

        public static ProductInventory Product2Inventory => new ProductInventory()
            {InStoreQuantity = 3, OrderedQuantity = 0, SuretyQuantity = 1, ProductId = Product2Equipment.Id };

        public static ProductInventory ProductToDeleteInventory => new ProductInventory()
            { InStoreQuantity = 3, OrderedQuantity = 0, SuretyQuantity = 1, ProductId = ProductToRemove.Id };
    }
}
