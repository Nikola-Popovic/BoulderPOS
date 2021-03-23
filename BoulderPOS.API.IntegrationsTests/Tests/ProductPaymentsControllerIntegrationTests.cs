using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using BoulderPOS.API.IntegrationsTests.DataSeed;
using BoulderPOS.API.Models;
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
            Assert.Contains(payments, payment => payment.CustomerId == CustomerSeeder.Customer1.Id);
            Assert.Contains(payments, payment => payment.ProductId == ProductSeeder.Product1Food.Id);
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
    }
}
