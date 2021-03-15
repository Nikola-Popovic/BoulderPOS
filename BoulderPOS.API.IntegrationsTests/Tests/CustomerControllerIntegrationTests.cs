using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BoulderPOS.API.Models;
using Newtonsoft.Json;
using Xunit;

namespace BoulderPOS.API.IntegrationsTests.Tests
{
    public class CustomerControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _httpClient;

        public CustomerControllerIntegrationTests(CustomWebApplicationFactory<Startup> factory)
        {
            this._httpClient = factory.CreateClient();
        }

        [Fact]
        public async Task CanGetCustomers()
        {
            var httpResponse = await this._httpClient.GetAsync("/api/customers");

            httpResponse.EnsureSuccessStatusCode();

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var customers = JsonConvert.DeserializeObject<IEnumerable<Customer>>(stringResponse);

            Assert.Contains(customers, customer => customer.FirstName == "Martin");
            Assert.Contains(customers, customer => customer.LastName == "Lemieux");
        }

        [Fact]
        public async Task CanGetCustomersByExactPhoneNumber()
        {
            var httpResponse = await this._httpClient.GetAsync("/api/customers/search?phoneNumber=3182916583");

            httpResponse.EnsureSuccessStatusCode();

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var customers = JsonConvert.DeserializeObject<IEnumerable<Customer>>(stringResponse);

            Assert.Contains(customers, customer => customer.FirstName == "Jean");
        }

        [Fact]
        public async Task CanGetCustomersByPartialPhoneNumber()
        {
            var httpResponse = await this._httpClient.GetAsync("/api/customers/search?phoneNumber=318");

            httpResponse.EnsureSuccessStatusCode();

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var customers = JsonConvert.DeserializeObject<IEnumerable<Customer>>(stringResponse);
            
            Assert.Contains(customers, customer => customer.FirstName == "Jean");
        }

        [Fact]
        public async Task CanCreateCustomer()
        {
            var newCustomer = new Customer()
            {
                BirthDate = DateTime.Now, Email = "Leopold.Bernard@gmail.com", FirstName = "Leopold",
                LastName = "Bernard", PhoneNumber = "+134765943254", NewsletterSubscription = true
            };
            
            var customerString = JsonConvert.SerializeObject(newCustomer);
            var httpResponse = await this._httpClient.PostAsync("/api/customers", new StringContent(customerString, Encoding.UTF8, "application/json"));

            httpResponse.EnsureSuccessStatusCode();

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var createdCustomer = JsonConvert.DeserializeObject<Customer>(stringResponse);

            Assert.NotNull(createdCustomer);
            Assert.Equal(newCustomer.PhoneNumber, createdCustomer.PhoneNumber);
        }
    }
}
