using System.Linq;
using System.Threading.Tasks;
using BoulderPOS.API.Models;
using BoulderPOS.API.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BoulderPOS.API.Services
{
    public class CustomerEntriesService : ICustomerEntriesService
    {
        private readonly ApplicationDbContext _context;

        public CustomerEntriesService(ApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task<CustomerEntries> GetCustomerEntries(int customerId)
        {
            var customer = await _context.Customers.FindAsync(customerId);
            return customer.Entries;
        }

        public async Task<CustomerEntries> UpdateCustomerEntries(int customerId, CustomerEntries customerEntries)
        {
            
            _context.Entry(customerEntries).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerEntriesExist(customerId))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            return customerEntries;
        }

        public async Task<CustomerEntries> CreateCustomerEntries(CustomerEntries customerEntries)
        {
            var created = _context.CustomerEntries.Add(customerEntries);
            await _context.SaveChangesAsync();
            return created.Entity;
        }

        public async Task<CustomerEntries> AddCustomerEntries(int customerId, int quantity)
        {
            var entries = await GetCustomerEntries(customerId);

            if (entries == null)
            {
                return null;
            }

            entries.Quantity += quantity;
            return await UpdateCustomerEntries(customerId, entries);
        }

        public async Task<CustomerEntries> TakeCustomerEntries(int customerId, int quantity)
        {
            var entries = await GetCustomerEntries(customerId);

            if (entries == null || (entries.Quantity - quantity) < 0)
            {
                return null;
            }

            entries.Quantity -= quantity;
            return await UpdateCustomerEntries(customerId, entries);
        }

        public bool CustomerEntriesExist(int customerId)
        {
            return _context.CustomerEntries.Any(entries => entries.Customer.Id == customerId);
        }
    }
}
