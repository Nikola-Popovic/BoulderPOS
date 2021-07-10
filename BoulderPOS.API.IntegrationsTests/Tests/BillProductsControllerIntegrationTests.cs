using System;
using System.Collections.Generic;
using System.Linq;
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
    public class BillProductsControllerIntegrationTests :
        IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _httpClient;
        private readonly CustomWebApplicationFactory<Startup> _factory;
        private const string BillProductsApiPath = "/api/billProducts";

        public BillProductsControllerIntegrationTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _httpClient = factory.CreateClient();
        }

        [Fact]
        public async Task CanGetLatestProductPayments()
        {
            var httpResponse = await _httpClient.GetAsync(BillProductsApiPath);

            httpResponse.EnsureSuccessStatusCode();

            var responseString = await httpResponse.Content.ReadAsStringAsync();

            var payments = JsonConvert.DeserializeObject<IEnumerable<BillProduct>>(responseString);

            Assert.NotEmpty(payments);
            // Make sure the payments are in date order
            var paymentList = payments.ToList();
            Assert.NotEqual(paymentList[0].ProductId, PaymentSeeder.WalkinBillProduct.ProductId);
            Assert.Equal(paymentList[0].Id, PaymentSeeder.Customer1BillProduct.Id);
        }

        [Fact]
        public async Task CanGetProductPaymentById()
        {
            var httpResponse = await _httpClient.GetAsync($"{BillProductsApiPath}/{PaymentSeeder.Customer1BillProduct.Id}");

            httpResponse.EnsureSuccessStatusCode();

            var responseString = await httpResponse.Content.ReadAsStringAsync();

            var payment = JsonConvert.DeserializeObject<BillProduct>(responseString);

            Assert.NotNull(payment);
            Assert.Equal(CustomerSeeder.Customer1.Id, payment.CustomerId);
        }

        [Fact]
        public async Task CanUpdateProductPayment()
        {
            var toUpdate = PaymentSeeder.Customer1BillProduct;
            toUpdate.IsRefunded = true;

            var stringObj = JsonConvert.SerializeObject(toUpdate);
            var httpResponse = await _httpClient.PutAsync(
                $"{BillProductsApiPath}/{PaymentSeeder.Customer1BillProduct.Id}", Util.JsonStringContent(stringObj));

            httpResponse.EnsureSuccessStatusCode();

            var responseString = await httpResponse.Content.ReadAsStringAsync();
            var updatedObj = JsonConvert.DeserializeObject<BillProduct>(responseString);

            Assert.NotNull(updatedObj);
            Assert.Equal(toUpdate.IsRefunded, updatedObj.IsRefunded);
            // Make sure the UpdateTime is bigger
            Assert.True(updatedObj.UpdatedDateTime.CompareTo(toUpdate.UpdatedDateTime) == 1);

            using var scope = _factory.Services.CreateScope();
            var appDb = scope.ServiceProvider.GetService<ApplicationDbContext>();

            var updatedInDb = await appDb.BillProducts.FindAsync(PaymentSeeder.Customer1BillProduct.Id);
            
            Assert.Equal(toUpdate.IsRefunded, updatedInDb.IsRefunded);
        }

        [Fact]
        public async Task CanCreateProductPayment()
        {
            var paymentToCreate = new BillProduct()
            {
                CustomerId = CustomerSeeder.Customer2.Id,
                ProductId = ProductSeeder.Product1Food.Id,
                SellingPrice = ProductSeeder.Product1Food.Id,
                ProcessedDateTime = DateTime.Now.AddDays(-7)
            };

            var objString = JsonConvert.SerializeObject(paymentToCreate);
            var httpResponse = await _httpClient.PostAsync(BillProductsApiPath, Util.JsonStringContent(objString));

            httpResponse.EnsureSuccessStatusCode();

            var responseString = await httpResponse.Content.ReadAsStringAsync();
            var createdPayment = JsonConvert.DeserializeObject<BillProduct>(responseString);

            Assert.NotNull(createdPayment);
            // Assert default value is false
            Assert.Equal(paymentToCreate.SellingPrice, createdPayment.SellingPrice);
            Assert.False(createdPayment.IsRefunded);
        }

        [Fact]
        public async Task EntriesPaymentAddsEntriesToCustomer()
        {
            var scope = _factory.Services.CreateScope();
            await using var dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();
            var paymentToCreate = new BillProduct()
            {
                CustomerId = CustomerSeeder.Customer1.Id,
                Product = ProductSeeder.EntriesProduct,
                Quantity = 1
            };

            var objString = JsonConvert.SerializeObject(paymentToCreate);
            var httpResponse = await _httpClient.PostAsync(BillProductsApiPath, Util.JsonStringContent(objString));

            httpResponse.EnsureSuccessStatusCode();

            var responseString = await httpResponse.Content.ReadAsStringAsync();
            var createdPayment = JsonConvert.DeserializeObject<BillProduct>(responseString);
            Assert.NotNull(createdPayment);

            var entries = await dbContext.CustomerEntries.FindAsync(CustomerSeeder.Customer1.Id);
            var expectedQty = CustomerSeeder.Customer1Entries.Quantity + ProductSeeder.EntriesProduct.Quantity;
            Assert.Equal(expectedQty, entries.Quantity);
        }

        [Fact]
        public async Task SubscriptionPaymentAddsTimeToCustomerSubscription()
        {
            var scope = _factory.Services.CreateScope();
            await using var dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();
            var paymentToCreate = new BillProduct()
            {
                CustomerId = CustomerSeeder.CustomerWithValidSubscription.Id,
                Product = ProductSeeder.SubscriptionProduct,
                Quantity = 1
            };

            var objString = JsonConvert.SerializeObject(paymentToCreate);
            var httpResponse = await _httpClient.PostAsync(BillProductsApiPath, Util.JsonStringContent(objString));

            httpResponse.EnsureSuccessStatusCode();

            var responseString = await httpResponse.Content.ReadAsStringAsync();
            var createdPayment = JsonConvert.DeserializeObject<BillProduct>(responseString);
            Assert.NotNull(createdPayment);

            var subscription = await dbContext.CustomerSubscriptions.FirstOrDefaultAsync(c => c.CustomerId == CustomerSeeder.CustomerWithValidSubscription.Id);

            var expectedTime = CustomerSeeder.ValidSubscription.EndDate.AddMonths((int) ProductSeeder.SubscriptionProduct.Quantity);
            Assert.Equal(expectedTime, subscription.EndDate);
        }
    }
}
