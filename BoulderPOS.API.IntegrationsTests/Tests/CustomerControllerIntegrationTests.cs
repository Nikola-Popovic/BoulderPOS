using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
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
    public class CustomerControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _httpClient;
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public CustomerControllerIntegrationTests(CustomWebApplicationFactory<Startup> factory)
        {
            this._httpClient = factory.CreateClient();
            _factory = factory;
        }

        [Fact]
        public async Task CanGetCustomers()
        {
            var httpResponse = await this._httpClient.GetAsync("/api/customers");

            httpResponse.EnsureSuccessStatusCode();

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var customers = JsonConvert.DeserializeObject<IEnumerable<Customer>>(stringResponse);

            Assert.Contains(customers, customer => customer.FirstName == CustomerSeeder.Customer1.FirstName);
            Assert.Contains(customers, customer => customer.LastName == CustomerSeeder.Customer2.LastName);
        }

        [Fact]
        public async Task CanGetCustomersByExactPhoneNumber()
        {
            var searchString = $"/api/customers/search?phoneNumber={CustomerSeeder.Customer1.PhoneNumber.Substring(1,10)}";
            var httpResponse = await this._httpClient.GetAsync(searchString);

            httpResponse.EnsureSuccessStatusCode();

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var customers = JsonConvert.DeserializeObject<IEnumerable<Customer>>(stringResponse);

            Assert.Contains(customers, customer => customer.FirstName == CustomerSeeder.Customer1.FirstName);
        }

        [Fact]
        public async Task CanGetCustomersByPartialPhoneNumber()
        {
            var customerPartialPhone = CustomerSeeder.Customer1.PhoneNumber.Substring(1, 4);
            var searchString = $"/api/customers/search?phoneNumber={customerPartialPhone}";
            var httpResponse = await this._httpClient.GetAsync(searchString);

            httpResponse.EnsureSuccessStatusCode();

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var customers = JsonConvert.DeserializeObject<IEnumerable<Customer>>(stringResponse);
            
            Assert.Contains(customers, customer => customer.FirstName == CustomerSeeder.Customer1.FirstName);
        }

        [Fact]
        public async Task CanGetCustomerById()
        {
            var searchString = $"/api/customers/{CustomerSeeder.Customer2.Id}";
            var httpResponse = await this._httpClient.GetAsync(searchString);

            httpResponse.EnsureSuccessStatusCode();

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var customer = JsonConvert.DeserializeObject<Customer>(stringResponse);

            Assert.NotNull(customer);
            Assert.Equal(CustomerSeeder.Customer2.Email, customer.Email);
        }

        [Fact]
        public async Task CanCreateCustomer()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
                var newCustomer = new Customer()
                {
                    BirthDate = DateTime.Now,
                    Email = "Leopold.Bernard@gmail.com",
                    FirstName = "Leopold",
                    LastName = "Bernard",
                    PhoneNumber = "+134765943254",
                    NewsletterSubscription = true
                };

                var customerString = JsonConvert.SerializeObject(newCustomer);
                var httpResponse = await this._httpClient.PostAsync("/api/customers",
                    new StringContent(customerString, Encoding.UTF8, "application/json"));

                httpResponse.EnsureSuccessStatusCode();

                var stringResponse = await httpResponse.Content.ReadAsStringAsync();
                var createdCustomer = JsonConvert.DeserializeObject<Customer>(stringResponse);

                Assert.NotNull(createdCustomer);
                Assert.Equal(newCustomer.PhoneNumber, createdCustomer.PhoneNumber);

                var customerInDb = context?.Customers.Find(createdCustomer.Id);
                Assert.Equal(createdCustomer.Email, customerInDb?.Email);
            }
        }

        [Fact]
        public async Task WhenCustomerCreatedEntriesAreCreated()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
                var newCustomer = new Customer()
                {
                    BirthDate = DateTime.Now,
                    Email = "Marion.Paolo@gmail.com",
                    FirstName = "Marion",
                    LastName = "Paolo",
                    PhoneNumber = "+3549876542",
                    NewsletterSubscription = true
                };

                var customerString = JsonConvert.SerializeObject(newCustomer);
                var httpResponse = await this._httpClient.PostAsync("/api/customers",
                    new StringContent(customerString, Encoding.UTF8, "application/json"));

                httpResponse.EnsureSuccessStatusCode();

                var stringResponse = await httpResponse.Content.ReadAsStringAsync();
                var createdCustomer = JsonConvert.DeserializeObject<Customer>(stringResponse);

                Assert.NotNull(createdCustomer.Entries);

                var entries = await context.CustomerEntries.FirstAsync(entry => entry.CustomerId == createdCustomer.Id);
                Assert.NotNull(entries);
                Assert.Equal(0, entries.Quantity);
            }
        }

        [Fact]
        public async Task CanUpdateCustomer()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

                var customerToUpdate = CustomerSeeder.Customer1;
                customerToUpdate.Email = "lemieux.martin@business.com";

                var customerString = JsonConvert.SerializeObject(customerToUpdate);
                var httpResponse = await this._httpClient.PutAsync($"/api/customers/{customerToUpdate.Id}",
                    new StringContent(customerString, Encoding.UTF8, "application/json"));

                httpResponse.EnsureSuccessStatusCode();
                Assert.Equal(HttpStatusCode.NoContent, httpResponse.StatusCode);

                var userInDb = context?.Customers.Find(customerToUpdate.Id);
                Assert.Equal( customerToUpdate.Email, userInDb?.Email);
            }
        }

        [Fact]
        public async Task CanCheckInCustomerWithSubscription()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
                
                var httpResponse = await this._httpClient.PutAsync($"/api/customers/{CustomerSeeder.CustomerWithValidSubscription.Id}/checkin",
                    new StringContent(""));

                httpResponse.EnsureSuccessStatusCode();
                Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);

                var responseString = await httpResponse.Content.ReadAsStringAsync();
                Assert.Equal("true", responseString);

                // Make sure no entries were used
                var entries = await context.CustomerEntries.FindAsync(CustomerSeeder.CustomerWithValidSubscriptionEntries.Id);
                Assert.Equal(CustomerSeeder.CustomerWithValidSubscriptionEntries.Quantity, entries.Quantity);
            }
        }

        [Fact]
        public async Task CanCheckInCustomerWithEntries()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

                var httpResponse = await this._httpClient.PutAsync($"/api/customers/{CustomerSeeder.Customer1.Id}/checkin",
                    new StringContent(""));

                httpResponse.EnsureSuccessStatusCode();
                Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);

                var responseString = await httpResponse.Content.ReadAsStringAsync();
                Assert.Equal("true", responseString);

                // Make sure only one entry was used
                var entries = await context.CustomerEntries.FindAsync(CustomerSeeder.Customer1Entries.Id);
                Assert.Equal((CustomerSeeder.Customer1Entries.Quantity - 1), entries.Quantity);
            }
        }

        [Fact]
        public async Task CheckinWithInvalidSubscriptionReturnsFalse()
        {
            var httpResponse = await this._httpClient.PutAsync($"/api/customers/{CustomerSeeder.CustomerWithInvalidSubscription.Id}/checkin",
                new StringContent(""));

            httpResponse.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);

            var responseString = await httpResponse.Content.ReadAsStringAsync();
            Assert.Equal("false", responseString);
        }

        [Fact]
        public async Task CheckinWithNoEntriesReturnsFalse()
        {
            var httpResponse = await this._httpClient.PutAsync($"/api/customers/{CustomerSeeder.CustomerWithNoEntries.Id}/checkin",
                new StringContent(""));

            httpResponse.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);

            var responseString = await httpResponse.Content.ReadAsStringAsync();
            Assert.Equal("false", responseString);
        }


        [Fact]
        public async Task CanDeleteUser()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

                var customerToDelete = CustomerSeeder.CustomerToDelete;
                
                var httpResponse = await this._httpClient.DeleteAsync($"/api/customers/{customerToDelete.Id}");

                httpResponse.EnsureSuccessStatusCode();
                Assert.Equal(HttpStatusCode.NoContent, httpResponse.StatusCode);

                var userInDb = context?.Customers.Find(customerToDelete.Id);
                Assert.Null(userInDb);
            }
        }
    }
}
