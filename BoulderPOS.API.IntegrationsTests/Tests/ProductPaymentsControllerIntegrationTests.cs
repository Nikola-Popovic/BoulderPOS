using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BoulderPOS.API.IntegrationsTests.DataSeed;
using BoulderPOS.API.Models;
using BoulderPOS.API.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Xunit;

namespace BoulderPOS.API.IntegrationsTests.Tests
{
    public class ProductPaymentsControllerIntegrationTests :
        IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _httpClient;
        private readonly CustomWebApplicationFactory<Startup> _factory;
        private const string ProductPaymentApiPath = "/api/payments";

        public ProductPaymentsControllerIntegrationTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _httpClient = factory.CreateClient();
        }

        [Fact]
        public async Task CanGetLatestProductPayments()
        {
            var httpResponse = await _httpClient.GetAsync(ProductPaymentApiPath);

            httpResponse.EnsureSuccessStatusCode();

            var responseString = await httpResponse.Content.ReadAsStringAsync();

            var payments = JsonConvert.DeserializeObject<IEnumerable<ProductPayment>>(responseString);

            Assert.NotEmpty(payments);
            // Make sure the payments are in date order
            var paymentList = payments.ToList();
            Assert.NotEqual(paymentList[0].ProductId, PaymentSeeder.WalkinFoodPayment.ProductId);
            Assert.Equal(paymentList[0].Id, PaymentSeeder.Customer1Payment.Id);
        }

        [Fact]
        public async Task CanGetProductPaymentById()
        {
            var httpResponse = await _httpClient.GetAsync($"{ProductPaymentApiPath}/{PaymentSeeder.Customer1Payment.Id}");

            httpResponse.EnsureSuccessStatusCode();

            var responseString = await httpResponse.Content.ReadAsStringAsync();

            var payment = JsonConvert.DeserializeObject<ProductPayment>(responseString);

            Assert.NotNull(payment);
            Assert.Equal(CustomerSeeder.Customer1.Id, payment.CustomerId);
        }

        [Fact]
        public async Task CanUpdateProductPayment()
        {
            var toUpdate = PaymentSeeder.Customer1Payment;
            toUpdate.IsRefunded = true;

            var stringObj = JsonConvert.SerializeObject(toUpdate);
            var httpResponse = await _httpClient.PutAsync(
                $"{ProductPaymentApiPath}/{PaymentSeeder.Customer1Payment.Id}", Util.JsonStringContent(stringObj));

            httpResponse.EnsureSuccessStatusCode();

            var responseString = await httpResponse.Content.ReadAsStringAsync();
            var updatedObj = JsonConvert.DeserializeObject<ProductPayment>(responseString);

            Assert.NotNull(updatedObj);
            Assert.Equal(toUpdate.IsRefunded, updatedObj.IsRefunded);
            // Make sure the UpdateTime is bigger
            Assert.True(updatedObj.UpdatedDateTime.CompareTo(toUpdate.UpdatedDateTime) == 1);

            using var scope = _factory.Services.CreateScope();
            var appDb = scope.ServiceProvider.GetService<ApplicationDbContext>();

            var updatedInDb = await appDb.ProductPayments.FindAsync(PaymentSeeder.Customer1Payment.Id);
            
            Assert.Equal(toUpdate.IsRefunded, updatedInDb.IsRefunded);
        }

        [Fact]
        public async Task CanCreateProductPayment()
        {
            var paymentToCreate = new ProductPayment()
            {
                CustomerId = CustomerSeeder.Customer2.Id,
                ProductId = ProductSeeder.Product1Food.Id,
                SellingPrice = ProductSeeder.Product1Food.Id,
                ProcessedDateTime = DateTime.Now.AddDays(-7)
            };

            var objString = JsonConvert.SerializeObject(paymentToCreate);
            var httpResponse = await _httpClient.PostAsync(ProductPaymentApiPath, Util.JsonStringContent(objString));

            httpResponse.EnsureSuccessStatusCode();

            var responseString = await httpResponse.Content.ReadAsStringAsync();
            var createdPayment = JsonConvert.DeserializeObject<ProductPayment>(responseString);

            Assert.NotNull(createdPayment);
            // Assert default value is false
            Assert.Equal(paymentToCreate.SellingPrice, createdPayment.SellingPrice);
            Assert.False(createdPayment.IsRefunded);
        }
    }
}
