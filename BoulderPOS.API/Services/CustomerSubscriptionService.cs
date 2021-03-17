using System;
using System.Linq;
using System.Threading.Tasks;
using BoulderPOS.API.Models;
using BoulderPOS.API.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BoulderPOS.API.Services
{
    public class CustomerSubscriptionService : ICustomerSubscriptionService
    {
        private readonly ApplicationDbContext _context;

        public CustomerSubscriptionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CustomerSubscription> GetCustomerSubscription(int customerId)
        {
            var customer = await _context.Customers.FindAsync(customerId);
            return customer.Subscription;
        }

        public async Task<CustomerSubscription> UpdateCustomerSubscription(int customerId, CustomerSubscription customerSubscription)
        {
             _context.Entry(customerSubscription).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerSubscriptionExist(customerId))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            return customerSubscription;
        }

        public async Task<CustomerSubscription> CreateCustomerSubscription(CustomerSubscription customerSubscription)
        {
            var created =_context.CustomerSubscriptions.Add(customerSubscription);
            await _context.SaveChangesAsync();
            return created.Entity;
        }

        public async Task<CustomerSubscription> DeleteCustomerSubscription(int customerId)
        {
            var customerSubscription = await _context.CustomerSubscriptions.FindAsync(customerId);
            
            if (customerSubscription == null) {
                return null;
            }
            _context.CustomerSubscriptions.Remove(customerSubscription);
            await _context.SaveChangesAsync();

            return customerSubscription;

        }

        public async Task<bool> HasValidCustomerSubscription(int customerId)
        {
            var subscription = await GetCustomerSubscription(customerId);

            if (subscription.EndDate >= DateTime.Today)
            {
                return true;
            }

            return false;
        }

        public async Task<CustomerSubscription> AddCustomerSubscription(int customerId, int timeInMonth)
        {
            var subscription = await GetCustomerSubscription(customerId);
            var now = DateTime.Now;
            if (subscription == null)
            {
                return await CreateCustomerSubscription(new CustomerSubscription(customerId, now, now.AddMonths(timeInMonth)));
            } else if (subscription.EndDate < DateTime.Now)
            {
                // Update start and end
                subscription.StartDate = now;
                subscription.EndDate = now.AddMonths(timeInMonth);
                return await UpdateCustomerSubscription(customerId, subscription);
            }
            else
            {
                // Add Time
                subscription.EndDate = subscription.EndDate.AddMonths(timeInMonth);
                return await UpdateCustomerSubscription(customerId, subscription);
            }
        }

        public bool CustomerSubscriptionExist(int customerId)
        {
            return _context.CustomerSubscriptions.Any(e => e.CustomerId == customerId);
        }
    }
}
