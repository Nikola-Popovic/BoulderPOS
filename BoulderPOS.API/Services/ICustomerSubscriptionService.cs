using System.Threading.Tasks;
using BoulderPOS.API.Models;

namespace BoulderPOS.API.Services
{
    public interface ICustomerSubscriptionService
    {
        Task<CustomerSubscription> GetCustomerSubscription(int customerId);

        Task<CustomerSubscription> UpdateCustomerSubscription(int customerId, CustomerSubscription customerSubscription);

        Task<CustomerSubscription> CreateCustomerSubscription(CustomerSubscription customerSubscription);

        Task<CustomerSubscription> DeleteCustomerSubscription(int customerId);

        Task<bool> HasValidCustomerSubscription(int customerId);

        Task<CustomerSubscription> AddCustomerSubscription(int customerId, int timeInMonth);

        // TODO: Auto-renewal cron task

        bool CustomerSubscriptionExist(int customerId);
    }
}
