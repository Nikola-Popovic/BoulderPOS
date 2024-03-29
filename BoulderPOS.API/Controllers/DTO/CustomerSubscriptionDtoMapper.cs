﻿using System;
using System.Globalization;
using BoulderPOS.API.Models;

namespace BoulderPOS.API.Controllers.DTO
{
    public static class CustomerSubscriptionDtoMapper
    {
        public static CustomerSubscription ToCustomerSubscription(this CustomerSubscriptionDto customerDto)
        {
            return new CustomerSubscription()
            {
                AutoRenewal = customerDto.AutoRenewal,
                CustomerId = customerDto.CustomerId,
                StartDate = DateTime.ParseExact(customerDto.StartDate.Substring(0, 10), "yyyy-MM-dd", CultureInfo.InvariantCulture),
                EndDate = DateTime.ParseExact(customerDto.EndDate.Substring(0, 10), "yyyy-MM-dd", CultureInfo.InvariantCulture)
            };
        }
    }
}
