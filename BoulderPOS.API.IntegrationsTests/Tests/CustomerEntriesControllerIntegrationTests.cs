using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xunit;

namespace BoulderPOS.API.IntegrationsTests.Tests
{
    public class CustomerEntriesControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _httpClient;

        public CustomerEntriesControllerIntegrationTests(CustomWebApplicationFactory<Startup> factory)
        {
            _httpClient = factory.CreateClient();
        }
    }
}
