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
    public class ProductCategoriesControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _httpClient;
        private readonly CustomWebApplicationFactory<Startup> _factory;
        private const string CategoriesApiPath = "/api/categories";

        public ProductCategoriesControllerIntegrationTests(CustomWebApplicationFactory<Startup> factory)
        {
            _httpClient = factory.CreateClient();
            _factory = factory;
        }

        [Fact]
        public async Task CanGetCategories()
        {
            var httpResponse = await _httpClient.GetAsync(CategoriesApiPath);

            httpResponse.EnsureSuccessStatusCode();

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var categories = JsonConvert.DeserializeObject<IEnumerable<ProductCategory>>(stringResponse);

            Assert.Contains(categories, category => category.Name == ProductSeeder.FoodCategory.Name);
            Assert.Contains(categories, category => category.Name == ProductSeeder.EquipmentCategory.Name);
        }

        [Fact]
        public async Task CanGetCategoryById()
        {
            var pathString = $"{CategoriesApiPath}/{ProductSeeder.FoodCategory.Id}";
            var httpResponse = await this._httpClient.GetAsync(pathString);

            httpResponse.EnsureSuccessStatusCode();

            var responseString = await httpResponse.Content.ReadAsStringAsync();
            var category = JsonConvert.DeserializeObject<ProductCategory>(responseString);

            Assert.NotNull(category);
            Assert.Equal(ProductSeeder.FoodCategory.Name, category.Name);
        }


        [Fact]
        public async Task GetProductsByCategory()
        {
            var pathString = $"{CategoriesApiPath}/{ProductSeeder.FoodCategory.Id}/products";
            var httpResponse = await this._httpClient.GetAsync(pathString);

            httpResponse.EnsureSuccessStatusCode();

            var responseString = await httpResponse.Content.ReadAsStringAsync();
            var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(responseString);

            Assert.Contains(products, category => category.Name == ProductSeeder.Product1Food.Name);
        }

        [Fact]
        public async Task CanCreateCategory()
        {
            var category = new ProductCategory() {Name = "Beverage", IconName = ""};
            var categoryString = JsonConvert.SerializeObject(category);
            var httpResponse = await this._httpClient.PostAsync(CategoriesApiPath,
                Util.JsonStringContent(categoryString));

            httpResponse.EnsureSuccessStatusCode();

            var responseString = await httpResponse.Content.ReadAsStringAsync();
            var createdObj = JsonConvert.DeserializeObject<ProductCategory>(responseString);

            Assert.NotNull(createdObj);
            Assert.Equal(category.Name, createdObj.Name);
        }

        [Fact]
        public async Task CanUpdateCategory()
        {
            using var scope = _factory.Services.CreateScope();
            var appDb = scope.ServiceProvider.GetService<ApplicationDbContext>();

            var toUpdate = ProductSeeder.FoodCategory;
            toUpdate.IconName = "/img/icon2";

            var categoryString = JsonConvert.SerializeObject(toUpdate);
            var httpResponse = await this._httpClient.PutAsync($"{CategoriesApiPath}/{toUpdate.Id}",
                Util.JsonStringContent(categoryString));

            httpResponse.EnsureSuccessStatusCode();

            var responseString = await httpResponse.Content.ReadAsStringAsync();
            var updated = JsonConvert.DeserializeObject<ProductCategory>(responseString);

            Assert.NotNull(updated);
            Assert.Equal(toUpdate.IconName, updated.IconName);

            var updatedInDb = await appDb.ProductCategories.FindAsync(toUpdate.Id);

            Assert.Equal(toUpdate.IconName, updatedInDb.IconName);
        }

        [Fact]
        public async Task CanDeleteCategory()
        {
            using var scope = _factory.Services.CreateScope();
            var appDb = scope.ServiceProvider.GetService<ApplicationDbContext>();
            
            var httpResponse = await this._httpClient.DeleteAsync($"{CategoriesApiPath}/{ProductSeeder.CategoryToDelete.Id}");

            httpResponse.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.NoContent,httpResponse.StatusCode);

            var deletedInBd = await appDb.ProductCategories.FirstOrDefaultAsync(category => category.Id == ProductSeeder.CategoryToDelete.Id);

            Assert.NotEqual(ProductSeeder.CategoryToDelete, deletedInBd);
        }
    }
}
