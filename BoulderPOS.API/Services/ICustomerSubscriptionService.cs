using System.Threading.Tasks;
using BoulderPOS.API.Models;

namespace BoulderPOS.API.Services
{
    public interface ICustomerSubscriptionService
    {
        Task<CustomerSubscription> GetCustomerSubscription(int customerId);

        Task<CustomerSubscription> UpdateCustomerSubscription(int customerId, CustomerSubscription customerSubscription);

        Task<CustomerSubscription> CreateCustomerSubscription(CustomerSubscription customerSubscription);

        Task<CustomerSubscription> DeleteCustomerSubscription(int id);

        Task<CustomerSubscription> AddCustomerSubscription(int customerId, int timeInMonth);

        bool CustomerSubscriptionExist(int customerId);
    }
}
