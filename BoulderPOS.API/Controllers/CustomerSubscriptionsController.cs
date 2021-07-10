using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BoulderPOS.API.Models;
using BoulderPOS.API.Models.DTO;
using BoulderPOS.API.Services;

namespace BoulderPOS.API.Controllers
{
    [Route("api/subscriptions")]
    [ApiController]
    public class CustomerSubscriptionsController : ControllerBase
    {
        private readonly ICustomerSubscriptionService _subscriptionService;

        public CustomerSubscriptionsController(ICustomerSubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        // GET: api/subscriptions/5
        [HttpGet("{customerId}")]
        public async Task<ActionResult<CustomerSubscription>> GetCustomerSubscription(int customerId)
        {
            var subscription = await _subscriptionService.GetCustomerSubscription(customerId);

            if (subscription == null)
            {
                return NotFound();
            }

            return Ok(subscription);
        }

        // GET: api/subscriptions/5/isValid
        [HttpGet("{customerId}/isValid")]
        public async Task<ActionResult<bool>> GetCustomerSubscriptionIsValid(int customerId)
        {
            var subscription = await _subscriptionService.HasValidCustomerSubscription(customerId);

            return Ok(subscription);
        }

        // PUT: api/subscriptions/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{customerId}")]
        public async Task<ActionResult<CustomerSubscription>> PutCustomerSubscription(int customerId, CustomerSubscription customerSubscription)
        {
            var updated = await _subscriptionService.UpdateCustomerSubscription(customerId, customerSubscription);

            if (updated == null)
            {
                return NotFound();
            }

            return Ok(updated);
            
        }


        // POST: api/subscriptions/5/add
        // Its possible to add more granular time control in the future with timeInYears, timeInDays, etc.
        [HttpPost("{customerId}/add")]
        public async Task<ActionResult<CustomerSubscription>> AddCustomerSubscription(int customerId, [FromQuery] int timeInMonth)
        {
            var updated = await _subscriptionService.AddCustomerSubscription(customerId, timeInMonth); 

            return updated;
        }


        // POST: api/subscriptions
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<CustomerSubscription>> PostCustomerSubscription(CustomerSubscriptionDto customerSubscriptionDto)
        {
            var subscription = await _subscriptionService.CreateCustomerSubscription(customerSubscriptionDto.ToCustomerSubscription());

            return CreatedAtAction("GetCustomerSubscription", new { customerId = subscription.CustomerId }, subscription);
        }

        // DELETE: api/subscriptions/5
        [HttpDelete("{customerId}")]
        public async Task<ActionResult<CustomerSubscription>> DeleteCustomerSubscription(int customerId)
        {
            await _subscriptionService.DeleteCustomerSubscription(customerId);

            return NoContent();
        }

    }
}
