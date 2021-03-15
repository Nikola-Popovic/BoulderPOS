using System;
using BoulderPOS.API.IntegrationsTests.DataSeed;
using BoulderPOS.API.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BoulderPOS.API.IntegrationsTests
{
    public class CustomWebApplicationFactory<TStartup>
        : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            
            builder.ConfigureServices(services =>
            {
                // For in memory database use : .AddEntityFrameworkInMemoryDatabase
                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkNpgsql()
                    .BuildServiceProvider();

                services.AddDbContext<ApplicationDbContext>((options, context) =>
                {
                    context.UseNpgsql(configuration.GetConnectionString("TestingDBConnection"));
                    // For in memory database use  context.UseInMemoryDatabase("InMemoryApplicationDb");
                    context.UseInternalServiceProvider(serviceProvider);
                });

                var sp = services.BuildServiceProvider();

                // Create a scope to obtain a reference to the database contexts
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var appDb = scopedServices.GetRequiredService<ApplicationDbContext>();

                    var logger = scopedServices.GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                    // Ensure the database is created.
                    appDb.Database.EnsureCreated();
                    appDb.Database.Migrate();
                    try
                    {
                        // Seed the database with some specific test data.
                        DataSeeder.PopulateTestData(appDb);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An error occurred seeding the " +
                                            "database with test messages. Error: {Message}", ex.Message);
                    }
                }
            });
        }
    }
}
