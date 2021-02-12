using System.Threading.Tasks;
using BoulderPOS.API.Models;

namespace BoulderPOS.API.Services
{
    public interface ICustomerEntriesService 
    {
        Task<CustomerEntries> GetCustomerEntries(int customerId);

        Task<CustomerEntries> UpdateCustomerEntries(int customerId, CustomerEntries customerEntries);

        Task<CustomerEntries> CreateCustomerEntries(CustomerEntries customerEntries);

        Task<CustomerEntries> AddCustomerEntries(int customerId, int quantity);

        Task<CustomerEntries> TakeCustomerEntries(int customerId, int quantity);

        bool CustomerEntriesExist(int customerId);
    }
}
