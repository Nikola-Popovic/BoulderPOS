using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using BoulderPOS.API.IntegrationsTests.DataSeed;
using BoulderPOS.API.Models;
using BoulderPOS.API.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Xunit;

namespace BoulderPOS.API.IntegrationsTests.Tests
{
    public class ProductInventoryControllerIntegrationsTests
        : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _httpClient;
        private readonly CustomWebApplicationFactory<Startup> _factory;
        private const string InventoryApiPath = "/api/ProductInventory";

        public ProductInventoryControllerIntegrationsTests(CustomWebApplicationFactory<Startup> factory)
        {
            _httpClient = factory.CreateClient();
            _factory = factory;
        }

        [Fact]
        public async Task CanGetInventories()
        {
            var httpResponse = await _httpClient.GetAsync(InventoryApiPath);

            httpResponse.EnsureSuccessStatusCode();
            
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var inventories = JsonConvert.DeserializeObject<IEnumerable<ProductInventory>>(stringResponse);

            Assert.Contains(inventories, inventory => inventory.InStoreQuantity == ProductSeeder.Product1Inventory.InStoreQuantity);
            Assert.Contains(inventories, inventory => inventory.SuretyQuantity == ProductSeeder.Product2Inventory.SuretyQuantity);
        }

        [Fact]
        public async Task CanGetProductInventoryById()
        {
            var pathString = $"{InventoryApiPath}/{ProductSeeder.Product1Inventory.ProductId}";
            var httpResponse = await this._httpClient.GetAsync(pathString);

            httpResponse.EnsureSuccessStatusCode();

            var responseString = await httpResponse.Content.ReadAsStringAsync();
            var inventory = JsonConvert.DeserializeObject<ProductInventory>(responseString);

            Assert.NotNull(inventory);
            Assert.Equal(ProductSeeder.Product1Inventory.InStoreQuantity, inventory.InStoreQuantity);
        }

        [Fact]
        public async Task CanUpdateProductInventory()
        {
            using var scope = _factory.Services.CreateScope();
            var appDb = scope.ServiceProvider.GetService<ApplicationDbContext>();

            var pathString = $"{InventoryApiPath}/{ProductSeeder.Product1Inventory.ProductId}";

            var toUpdate = ProductSeeder.Product1Inventory;
            toUpdate.OrderedQuantity += 5;

            var inventoryString = JsonConvert.SerializeObject(toUpdate);
            var httpResponse = await this._httpClient.PutAsync(pathString, Util.JsonStringContent(inventoryString));
            
            httpResponse.EnsureSuccessStatusCode();

            var responseString = await httpResponse.Content.ReadAsStringAsync();
            var inventory = JsonConvert.DeserializeObject<ProductInventory>(responseString);

            Assert.NotNull(inventory);
            Assert.Equal(toUpdate.OrderedQuantity, inventory.OrderedQuantity);

            var updatedInDb = await appDb.ProductInventory.FirstAsync(pinv => pinv.ProductId == toUpdate.ProductId);
            Assert.Equal(toUpdate.OrderedQuantity, updatedInDb.OrderedQuantity);
        }

        [Fact]
        public async Task CanCreateProductInventory()
        {
            var toCreate = new ProductInventory(ProductSeeder.ProductWithoutInventory.Id);
            var stringObj = JsonConvert.SerializeObject(toCreate);

            var httpResponse = await _httpClient.PostAsync(InventoryApiPath, Util.JsonStringContent(stringObj));

            httpResponse.EnsureSuccessStatusCode();

            var responseString = await httpResponse.Content.ReadAsStringAsync();
            var createObj = JsonConvert.DeserializeObject<ProductInventory>(responseString);

            Assert.NotNull(createObj);
            Assert.Equal(toCreate.ProductId, createObj.ProductId);
            Assert.Equal(toCreate.InStoreQuantity, createObj.InStoreQuantity);

        }


    }
}
