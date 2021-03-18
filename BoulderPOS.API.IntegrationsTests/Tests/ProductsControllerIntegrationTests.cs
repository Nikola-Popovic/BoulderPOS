using System.Collections.Generic;
using System.Net;
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
    public class ProductsControllerIntegrationTests 
        : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        private readonly HttpClient _httpClient;
        private const string ProductsApiPath = "/api/products";

        public ProductsControllerIntegrationTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _httpClient = _factory.CreateClient();
        }

        [Fact]
        public async Task CanGetProducts()
        {
            var httpResponse = await _httpClient.GetAsync(ProductsApiPath);

            httpResponse.EnsureSuccessStatusCode();

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(stringResponse);

            Assert.NotEmpty(products);
            Assert.Contains(products, product => product.Name == ProductSeeder.Product1Food.Name);
            Assert.Contains(products, product => product.Name == ProductSeeder.Product2Equipment.Name);
        }

        [Fact]
        public async Task CanGetProductById()
        {
            var httpResponse = await _httpClient.GetAsync($"{ProductsApiPath}/{ProductSeeder.Product1Food.Id}");

            httpResponse.EnsureSuccessStatusCode();

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<Product>(stringResponse);

            Assert.NotNull(product);
            Assert.Equal(ProductSeeder.Product1Food.Price, product.Price);
        }

        [Fact]
        public async Task CanGetUpdateProduct()
        {
            using var scope = _factory.Services.CreateScope();
            var appDb = scope.ServiceProvider.GetService<ApplicationDbContext>();
            var productToUpdate = ProductSeeder.Product2Equipment; 
            productToUpdate.Price = new decimal(130.50);

            var stringObj = JsonConvert.SerializeObject(productToUpdate);
            var httpResponse = await _httpClient.PutAsync($"{ProductsApiPath}/{productToUpdate.Id}", Util.JsonStringContent(stringObj));

            httpResponse.EnsureSuccessStatusCode();

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<Product>(stringResponse);

            Assert.NotNull(product);
            Assert.Equal(productToUpdate.Price, product.Price);

            var productInDb = await appDb.Products.FindAsync(productToUpdate.Id);
            Assert.Equal(productToUpdate.Price, productInDb.Price);
        }

        [Fact]
        public async Task CanCreateProduct()
        {
            using var scope = _factory.Services.CreateScope();
            var appDb = scope.ServiceProvider.GetService<ApplicationDbContext>();

            var newProduct = new Product()
            {
                CategoryId = ProductSeeder.FoodCategory.Id,
                Name = "H20",
                Price = new decimal(1.00)
            };
            var stringObj = JsonConvert.SerializeObject(newProduct);
            var httpResponse = await _httpClient.PostAsync(ProductsApiPath, Util.JsonStringContent(stringObj));

            httpResponse.EnsureSuccessStatusCode();

            var responseAsString = await httpResponse.Content.ReadAsStringAsync();
            var createdProduct = JsonConvert.DeserializeObject<Product>(responseAsString);

            Assert.NotNull(createdProduct);
            Assert.Equal(newProduct.Name, createdProduct.Name);

            var productInDb = await appDb.Products.FirstAsync(product => product.Name == createdProduct.Name);
            Assert.Equal(createdProduct.Price, productInDb.Price);
        }
        
        [Fact]
        // Todo : Bug fix code 500
        public async Task CanCreateProductWithInventory()
        {
            using var scope = _factory.Services.CreateScope();
            var appDb = scope.ServiceProvider.GetService<ApplicationDbContext>();

            var newProduct = new Product()
            {
                CategoryId = ProductSeeder.FoodCategory.Id,
                Name = "Ramen",
                Price = new decimal(7.50)
            };
            var stringObj = JsonConvert.SerializeObject(newProduct);
            var httpResponse = await _httpClient.PostAsync($"{ProductsApiPath}?createInventory={true}", Util.JsonStringContent(stringObj));

            httpResponse.EnsureSuccessStatusCode();

            var responseAsString = await httpResponse.Content.ReadAsStringAsync();
            var createdProduct = JsonConvert.DeserializeObject<Product>(responseAsString);

            Assert.NotNull(createdProduct);
            Assert.Equal(newProduct.Name, createdProduct.Name);

            var productInDb = await appDb.Products.FirstAsync(product => product.Name == createdProduct.Name);
            Assert.Equal(createdProduct.Price, productInDb.Price);
        }

        [Fact]
        public async Task CanDeleteProduct()
        {
            using var scope = _factory.Services.CreateScope();
            var appDb = scope.ServiceProvider.GetService<ApplicationDbContext>();

            var httpResponse = await this._httpClient.DeleteAsync($"{ProductsApiPath}/{ProductSeeder.ProductToRemove.Id}");

            httpResponse.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.NoContent, httpResponse.StatusCode);

            var deletedInBd = await appDb.Products.FirstOrDefaultAsync(p => p.Id == ProductSeeder.ProductToRemove.Id);

            Assert.NotEqual(ProductSeeder.ProductToRemove, deletedInBd);
        }
        // When a product is deleted the inventory is also deleted
    }
}
