using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BoulderPOS.API.Models;
using Newtonsoft.Json;
using Xunit;

namespace BoulderPOS.API.IntegrationsTests.Tests
{
    public class BillControllerIntegrationTests : 
        IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _httpClient;
        private readonly CustomWebApplicationFactory<Startup> _factory;
        private const string BillsApiPath = "/api/bills";

        public BillControllerIntegrationTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _httpClient = factory.CreateClient();
        }

        [Fact]
        public async Task CanGetLatestBills()
        {
            var httpResponse = await _httpClient.GetAsync(BillsApiPath);

            httpResponse.EnsureSuccessStatusCode();

            var responseString = await httpResponse.Content.ReadAsStringAsync();

            var payments = JsonConvert.DeserializeObject<IEnumerable<Bill>>(responseString);

            Assert.NotNull(payments);

            //Asserts
        }
    }
}
