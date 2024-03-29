﻿using System.Collections.Generic;
using System.Threading.Tasks;
using BoulderPOS.API.Models;

namespace BoulderPOS.API.Services
{
    public interface ICustomerService
    {
        public Task<IEnumerable<Customer>> GetCustomers();

        public Task<Customer> GetCustomer(int id);

        public Task<IEnumerable<Customer>> GetCustomersByPhone(string phoneNumber);

        public Task<IEnumerable<Customer>> GetCustomersByCustomerInfo(string customerInfo);
        
        public Task<bool> CheckInCustomer(int id);

        public Task<Customer> UpdateCustomer(int id, Customer customer);

        public Task<Customer> CreateCustomer(Customer customer);

        public Task<Customer> DeleteCustomer(int id);

        public bool CustomerExists(int id);

        // Todo : Customer Exists By Email / By Phone Number
    }
}
