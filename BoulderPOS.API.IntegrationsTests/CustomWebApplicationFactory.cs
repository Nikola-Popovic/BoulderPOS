using System;
using System.Linq;
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
        private ServiceProvider _serviceProvider;

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            
            builder.ConfigureServices(services =>
            {
                // The following line is to use a different ApplicationDbContext for tests
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                         typeof(DbContextOptions<ApplicationDbContext>));

                services.Remove(descriptor);

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

                _serviceProvider = services.BuildServiceProvider();

                // Create a scope to obtain a reference to the database contexts
                using var scope = _serviceProvider.CreateScope();
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
            });
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            using var scope = _serviceProvider.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var appDb = scopedServices.GetRequiredService<ApplicationDbContext>();
            appDb.Database.EnsureDeleted();
            appDb.Dispose();
            scope.Dispose();
            _serviceProvider.Dispose();
        }
    }
}
