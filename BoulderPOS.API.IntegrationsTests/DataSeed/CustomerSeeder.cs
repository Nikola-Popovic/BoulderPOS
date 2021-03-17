using System;
using System.Collections.Generic;
using System.Text;
using BoulderPOS.API.Models;
using BoulderPOS.API.Persistence;

namespace BoulderPOS.API.IntegrationsTests.DataSeed
{
    public static class CustomerSeeder
    {
        public static CustomerEntries Customer1Entries => new CustomerEntries(Customer1.Id, 10) { Id = 1 };
        public static CustomerEntries Customer2Entries => new CustomerEntries(Customer2.Id, 5, true) { Id = 2 };

        public static Customer Customer1 => new Customer()
        {
            BirthDate = new DateTime(1996, 10, 29),
            Email = "Martin.Lemieux@mail.com",
            FirstName = "Martin",
            LastName = "Lemieux",
            Id = 1,
            NewsletterSubscription = false,
            PhoneNumber = "+15143184567"
        };

        public static Customer Customer2 => new Customer()
        {
            BirthDate = new DateTime(1963, 06, 14),
            Email = "Jean.Guilmette@dash.com",
            FirstName = "Jean",
            LastName = "Guilmette",
            Id = 2,
            NewsletterSubscription = true,
            PhoneNumber = "+13182916583"
        };

        public static Customer CustomerToDelete => new Customer()
        {
            BirthDate = DateTime.Now,
            Email = "test.useur@mail.com",
            FirstName = "Test",
            LastName = "Customer",
            Id = 3,
            NewsletterSubscription = false,
            PhoneNumber = "+1234567890"
        };
    }
}
