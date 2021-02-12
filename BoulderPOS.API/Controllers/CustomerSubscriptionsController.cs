using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BoulderPOS.API.Models;
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
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerSubscription>> GetCustomerSubscription(int customerId)
        {
            var subscription = await _subscriptionService.GetCustomerSubscription(customerId);

            if (subscription == null)
            {
                return NotFound();
            }

            return Ok(subscription);
        }

        // PUT: api/subscriptions/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomerSubscription(int customerId, CustomerSubscription customerSubscription)
        {
            var updated = await _subscriptionService.UpdateCustomerSubscription(customerId, customerSubscription);

            if (updated == null)
            {
                return NotFound();
            }

            return Ok(updated);
            
        }


        // POST: api/subscriptions/5/add
        [HttpPost("{id}/add")]
        public async Task<ActionResult<CustomerSubscription>> AddCustomerSubscription(int customerId, int timeInMonth)
        {
            var updated = await _subscriptionService.AddCustomerSubscription(customerId, timeInMonth); 
            var deleted = await _subscriptionService.DeleteCustomerSubscription(customerId);

            return deleted;
        }


        // POST: api/subscriptions
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<CustomerSubscription>> PostCustomerSubscription(CustomerSubscription customerSubscription)
        {
            var subscription = await _subscriptionService.CreateCustomerSubscription(customerSubscription);

            return CreatedAtAction("GetCustomerSubscription", new { id = subscription.Id }, subscription);
        }

        // DELETE: api/subscriptions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CustomerSubscription>> DeleteCustomerSubscription(int customerId)
        {
            await _subscriptionService.DeleteCustomerSubscription(customerId);

            return NoContent();
        }

    }
}
