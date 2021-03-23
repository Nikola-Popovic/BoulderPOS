using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoulderPOS.API.Models;
using BoulderPOS.API.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BoulderPOS.API.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ApplicationDbContext _context;
        private readonly ICustomerEntriesService _entriesService;

        public CustomerService(ApplicationDbContext context, ICustomerEntriesService entriesService) 
        {
            _context = context;
            _entriesService = entriesService;
        }

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer> GetCustomer(int id)
        {
            return await _context.Customers.FindAsync(id); ;
        }

        public Task<IEnumerable<Customer>> GetCustomersByPhone(string phoneNumber)
        {
            var customers = _context.Customers.Where(customer => customer.PhoneNumber.Contains(phoneNumber)).AsEnumerable();
            return Task.FromResult(customers);
        }

        // Wouldn't be quite efficient but just in case it is desired
        public Task<IEnumerable<Customer>> GetCustomersByCustomerInfo(string customerInfo)
        {
            var customers = _context.Customers.Where(customer => 
                (customer.PhoneNumber.Any(char.IsDigit) &&   customer.PhoneNumber.Contains(customerInfo)) ||
                customer.FirstName.Equals(customerInfo) ||
                customer.LastName.Equals(customerInfo)
            ).AsEnumerable();
            return Task.FromResult(customers);
        }


        public async Task<Customer> UpdateCustomer(int id, Customer customer)
        {
           _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            return customer;
        }

        public async Task<Customer> CreateCustomer(Customer customer)
        {
            if (_context.Customers.Any())
            {
                customer.Id = await _context.Customers.MaxAsync(p => p.Id) + 1;
            }

            var created = _context.Customers.Add(customer).Entity;
            await _context.SaveChangesAsync();

            if (created.Entries == null)
            {
                created.Entries = await _entriesService.CreateCustomerEntries(new CustomerEntries(customer.Id));
            }

            return created;
        }

        public async Task<Customer> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return null;
            }
            

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            
            return customer;
        }

        public bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}
