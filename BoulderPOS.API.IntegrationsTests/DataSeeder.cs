using System;
using BoulderPOS.API.Models;
using BoulderPOS.API.Persistence;

namespace BoulderPOS.API.IntegrationsTests
{
    public static class DataSeeder
    {
        public static void PopulateTestData(ApplicationDbContext dbContext)
        {
            AddCustomers(dbContext);
            dbContext.SaveChanges();
        }

        private static void AddCustomers(ApplicationDbContext dbContext)
        {
            dbContext.Customers.Add(new Customer()
            {
                BirthDate = new DateTime(1996, 10, 29),
                Email = "Martin.Lemieux@mail.com",
                FirstName = "Martin",
                LastName = "Lemieux",
                Id = 1,
                NewsletterSubscription = false,
                PhoneNumber = "+15143184567"
            });
            dbContext.Customers.Add(new Customer()
            {
                BirthDate = new DateTime(1963, 06, 14),
                Email = "Jean.Guilmette@dash.com",
                FirstName = "Jean",
                LastName = "Guilmette",
                Id = 2,
                NewsletterSubscription = true,
                PhoneNumber = "+13182916583"
            });
        }
    }
}
