using System.Net.Http;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;
using BoulderPOS.API.IntegrationsTests.DataSeed;
using BoulderPOS.API.Models;
using BoulderPOS.API.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Xunit;

namespace BoulderPOS.API.IntegrationsTests.Tests
{
    public class CustomerEntriesControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _httpClient;
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public CustomerEntriesControllerIntegrationTests(CustomWebApplicationFactory<Startup> factory)
        {
            _httpClient = factory.CreateClient();
            _factory = factory;
        }


        [Fact]
        public async Task CanGetCustomerEntries()
        {
            var searchString = $"/api/entries/{CustomerSeeder.Customer2.Id}";
            var httpResponse = await this._httpClient.GetAsync(searchString);

            httpResponse.EnsureSuccessStatusCode();

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var customerEntries = JsonConvert.DeserializeObject<CustomerEntries>(stringResponse);

            Assert.NotNull(customerEntries);
            Assert.Equal(CustomerSeeder.Customer2Entries.Quantity, customerEntries.Quantity);
        }

        [Fact]
        public async Task CanAddCustomerEntries()
        {
            var previousQty = CustomerSeeder.Customer2Entries.Quantity;
            const int quantityToAdd = 5;

            var searchString = $"/api/entries/{CustomerSeeder.Customer2.Id}/add?quantity={quantityToAdd}";
            var httpResponse = await this._httpClient.PutAsync(searchString, new StringContent(""));

            httpResponse.EnsureSuccessStatusCode();

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var customerEntries = JsonConvert.DeserializeObject<CustomerEntries>(stringResponse);

            var updatedQty = previousQty + quantityToAdd;
            Assert.NotNull(customerEntries);
            Assert.Equal(updatedQty, customerEntries.Quantity);
        }

        [Fact]
        public async Task CanRemoveCustomerEntries()
        {
            var previousQty = CustomerSeeder.Customer1Entries.Quantity;
            const int quantityToRemove = 6;

            var searchString = $"/api/entries/{CustomerSeeder.Customer1.Id}/remove?quantity={quantityToRemove}";

            var httpResponse = await this._httpClient.PutAsync(searchString, new StringContent(""));

            httpResponse.EnsureSuccessStatusCode();

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var customerEntries = JsonConvert.DeserializeObject<CustomerEntries>(stringResponse);

            var updatedQty = previousQty - quantityToRemove;
            Assert.NotNull(customerEntries);
            Assert.Equal(updatedQty, customerEntries.Quantity);

        }

        [Fact]
        public async Task CannotRemoveWhenUnlimitedEntries()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
                var entries = context.CustomerEntries.Find(CustomerSeeder.Customer2Entries.Id);

                var previousQty = entries.Quantity;
                const int quantityToRemove = 6;

                var searchString = $"/api/entries/{CustomerSeeder.Customer2.Id}/remove?quantity={quantityToRemove}";

                var httpResponse = await this._httpClient.PutAsync(searchString, new StringContent(""));

                httpResponse.EnsureSuccessStatusCode();

                var stringResponse = await httpResponse.Content.ReadAsStringAsync();
                var customerEntries = JsonConvert.DeserializeObject<CustomerEntries>(stringResponse);
                
                Assert.NotNull(customerEntries);
                Assert.Equal(previousQty, customerEntries.Quantity);
            }
        }

        [Fact]
        public async Task CannotRemoveWhenSubscribed()
        {
            // Todo
        }
    }
}
