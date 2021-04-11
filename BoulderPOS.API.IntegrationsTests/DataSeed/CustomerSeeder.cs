using System;
using BoulderPOS.API.Models;

namespace BoulderPOS.API.IntegrationsTests.DataSeed
{
    public static class CustomerSeeder
    {
        public static CustomerEntries Customer1Entries => new CustomerEntries(Customer1.Id, 10) { Id = 1 };
        public static CustomerEntries Customer2Entries => new CustomerEntries(Customer2.Id, 5, true) { Id = 2 };

        public static CustomerEntries CustomerWithNoEntriesEntries => new CustomerEntries(CustomerWithNoEntries.Id, 0) { Id = 3 };

        public static CustomerEntries CustomerWithValidSubscriptionEntries =>
            new CustomerEntries(CustomerWithValidSubscription.Id, 7, false) {Id = 4};

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

        public static Customer CustomerWithNoEntries => new Customer()
        {
            BirthDate = DateTime.Now,
            Email = "igor.pop@mail.com",
            FirstName = "Igor",
            LastName = "Pop",
            Id = 3,
            NewsletterSubscription = false,
            PhoneNumber = "+19999999999"
        };
        
        public static Customer CustomerToDelete => new Customer()
        {
            BirthDate = DateTime.Now,
            Email = "test.useur@mail.com",
            FirstName = "Test",
            LastName = "Customer",
            Id = 4,
            NewsletterSubscription = false,
            PhoneNumber = "+1234567890"
        };

        public static Customer CustomerWithValidSubscription => new Customer()
        {
            BirthDate = new DateTime(1996, 10, 29),
            Email = "Jean.Dessin@mail.com",
            FirstName = "Jean",
            LastName = "Dessin",
            Id = 5,
            NewsletterSubscription = false,
            PhoneNumber = "+18976543214"
        };

        public static CustomerSubscription ValidSubscription => new CustomerSubscription()
        {
            CustomerId = 5,
            StartDate = DateTime.Today,
            EndDate = DateTime.Today.AddDays(30)
        };

        public static CustomerSubscription InvalidSubscription => new CustomerSubscription()
        {
            CustomerId = 6,
            StartDate = DateTime.Today.Subtract(TimeSpan.FromDays(33)),
            EndDate = DateTime.Today.Subtract(TimeSpan.FromDays(3))
        };

        public static Customer CustomerWithInvalidSubscription => new Customer()
        {
            BirthDate = new DateTime(1996, 10, 29),
            Email = "Luigi.Ferrari@fast.itl",
            FirstName = "Luigi",
            LastName = "Ferrari",
            Id = 6,
            NewsletterSubscription = false,
            PhoneNumber = "+6324567812"
        };
    }
}
