using System;
using System.Net.Http;
using System.Threading.Tasks;
using BoulderPOS.API.IntegrationsTests.DataSeed;
using BoulderPOS.API.Models;
using BoulderPOS.API.Models.DTO;
using BoulderPOS.API.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Xunit;

namespace BoulderPOS.API.IntegrationsTests.Tests
{
    public class CustomerSubscriptionIntegrationTests :
        IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        private readonly HttpClient _httpClient;
        private const string SubscriptionApiPath = "/api/subscriptions";

        public CustomerSubscriptionIntegrationTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _httpClient = factory.CreateClient();
        }

        [Fact]
        public async Task CanGetSubscription()
        {
            var httpResponse = await _httpClient.GetAsync($"{SubscriptionApiPath}/{CustomerSeeder.CustomerWithValidSubscription.Id}");

            httpResponse.EnsureSuccessStatusCode();

            var responseString = await httpResponse.Content.ReadAsStringAsync();
            var subscription = JsonConvert.DeserializeObject<CustomerSubscription>(responseString);

            Assert.NotNull(subscription);
            Assert.Equal(CustomerSeeder.ValidSubscription.EndDate, subscription.EndDate);
        }

        [Fact]
        public async Task WhenSubscriptionIsValidIsValidReturnsTrue()
        {
            var httpResponse = await _httpClient.GetAsync($"{SubscriptionApiPath}/{CustomerSeeder.CustomerWithValidSubscription.Id}/isValid");

            httpResponse.EnsureSuccessStatusCode();

            var responseString = await httpResponse.Content.ReadAsStringAsync();
            var subscriptionIsValid = JsonConvert.DeserializeObject<bool>(responseString);

            Assert.True(subscriptionIsValid);
        }

        [Fact]
        public async Task WhenSubscriptionIsInvalidIsValidReturnsFalse()
        {
            var httpResponse = await _httpClient.GetAsync($"{SubscriptionApiPath}/{CustomerSeeder.CustomerWithInvalidSubscription.Id}/isValid");

            httpResponse.EnsureSuccessStatusCode();

            var responseString = await httpResponse.Content.ReadAsStringAsync();
            var subscriptionIsValid = JsonConvert.DeserializeObject<bool>(responseString);

            Assert.False(subscriptionIsValid);
        }

        [Fact]
        public async Task CanUpdateSubscription()
        {
            using var scope = _factory.Services.CreateScope();
            var appDb = scope.ServiceProvider.GetService<ApplicationDbContext>();

            var toUpdate = CustomerSeeder.ValidSubscription;
            var newEndDate = DateTime.Today.AddMonths(6);
            toUpdate.EndDate = newEndDate;

            var stringObj = JsonConvert.SerializeObject(toUpdate);
            var httpResponse = await _httpClient.PutAsync(
                $"{SubscriptionApiPath}/{CustomerSeeder.CustomerWithValidSubscription.Id}",
                Util.JsonStringContent(stringObj));

            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var updatedSub = JsonConvert.DeserializeObject<CustomerSubscription>(stringResponse);

            Assert.NotNull(updatedSub);
            Assert.Equal(newEndDate, updatedSub.EndDate);

            var updatedInDb = await appDb.CustomerSubscriptions.FirstAsync( c => c.CustomerId == CustomerSeeder.ValidSubscription.CustomerId);
            Assert.Equal(newEndDate, updatedInDb.EndDate);
        }

        [Fact]
        public async Task CanAddTimeToSubscription()
        {
            using var scope = _factory.Services.CreateScope();
            var appDb = scope.ServiceProvider.GetService<ApplicationDbContext>();
            const int monthsToAdd = 1;
            var toUpdate = await appDb.CustomerSubscriptions.FirstAsync(c => c.CustomerId == CustomerSeeder.ValidSubscription.CustomerId);
            var expectedNewEndDate = toUpdate.EndDate.AddMonths(monthsToAdd);

            var queryString =
                $"{SubscriptionApiPath}/{CustomerSeeder.ValidSubscription.CustomerId}/add?timeInMonth={monthsToAdd}";
            var httpResponse = await _httpClient.PostAsync(queryString, new StringContent(""));
            httpResponse.EnsureSuccessStatusCode();

            var responseString = await httpResponse.Content.ReadAsStringAsync();
            var updatedSub = JsonConvert.DeserializeObject<CustomerSubscription>(responseString);

            Assert.NotNull(updatedSub);
            Assert.Equal(expectedNewEndDate, updatedSub.EndDate);
        }

        [Fact]
        public async Task CanCreateSubscription()
        {
            // using the format of Date.toISOString() of Javascript
            var startDate = "2021-03-22T12:54:54.55";
            var endDate = "2021-04-22T12:54:54.55";
            var customerSubscription = new CustomerSubscriptionDto()
            {
                AutoRenewal = false,
                CustomerId = CustomerSeeder.Customer1.Id,
                StartDate = startDate,
                EndDate = endDate
            };
            var stringObj = JsonConvert.SerializeObject(customerSubscription);
            var httpResponse = await _httpClient.PostAsync(SubscriptionApiPath, Util.JsonStringContent(stringObj));

            httpResponse.EnsureSuccessStatusCode();
            var responseString = await httpResponse.Content.ReadAsStringAsync();
            var createdSubscription = JsonConvert.DeserializeObject<CustomerSubscription>(responseString);

            Assert.Equal(3, createdSubscription.StartDate.Month);
            Assert.Equal(22, createdSubscription.StartDate.Day);
            Assert.Equal(2021, createdSubscription.StartDate.Year);

            Assert.Equal(4, createdSubscription.EndDate.Month);
            Assert.Equal(22, createdSubscription.EndDate.Day);
            Assert.Equal(2021, createdSubscription.EndDate.Year);
        }
    }
}
