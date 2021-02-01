using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BoulderPOS.API.Models;
using BoulderPOS.API.Persistence;

namespace BoulderPOS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerSubscriptionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CustomerSubscriptionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/CustomerSubscriptions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerSubscription>>> GetCustomerSubscriptions()
        {
            return await _context.CustomerSubscriptions.ToListAsync();
        }

        // GET: api/CustomerSubscriptions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerSubscription>> GetCustomerSubscription(int id)
        {
            var customerSubscription = await _context.CustomerSubscriptions.FindAsync(id);

            if (customerSubscription == null)
            {
                return NotFound();
            }

            return customerSubscription;
        }

        // PUT: api/CustomerSubscriptions/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomerSubscription(int id, CustomerSubscription customerSubscription)
        {
            if (id != customerSubscription.Id)
            {
                return BadRequest();
            }

            _context.Entry(customerSubscription).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerSubscriptionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CustomerSubscriptions
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<CustomerSubscription>> PostCustomerSubscription(CustomerSubscription customerSubscription)
        {
            _context.CustomerSubscriptions.Add(customerSubscription);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomerSubscription", new { id = customerSubscription.Id }, customerSubscription);
        }

        // DELETE: api/CustomerSubscriptions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CustomerSubscription>> DeleteCustomerSubscription(int id)
        {
            var customerSubscription = await _context.CustomerSubscriptions.FindAsync(id);
            if (customerSubscription == null)
            {
                return NotFound();
            }

            _context.CustomerSubscriptions.Remove(customerSubscription);
            await _context.SaveChangesAsync();

            return customerSubscription;
        }

        private bool CustomerSubscriptionExists(int id)
        {
            return _context.CustomerSubscriptions.Any(e => e.Id == id);
        }
    }
}
