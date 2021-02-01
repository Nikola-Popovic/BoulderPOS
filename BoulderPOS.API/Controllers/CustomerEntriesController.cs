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
    public class CustomerEntriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CustomerEntriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/CustomerEntries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerEntries>>> GetCustomerEntries()
        {
            return await _context.CustomerEntries.ToListAsync();
        }

        // GET: api/CustomerEntries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerEntries>> GetCustomerEntries(int id)
        {
            var customerEntries = await _context.CustomerEntries.FindAsync(id);

            if (customerEntries == null)
            {
                return NotFound();
            }

            return customerEntries;
        }

        // PUT: api/CustomerEntries/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomerEntries(int id, CustomerEntries customerEntries)
        {
            if (id != customerEntries.Id)
            {
                return BadRequest();
            }

            _context.Entry(customerEntries).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerEntriesExists(id))
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

        // POST: api/CustomerEntries
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<CustomerEntries>> PostCustomerEntries(CustomerEntries customerEntries)
        {
            _context.CustomerEntries.Add(customerEntries);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomerEntries", new { id = customerEntries.Id }, customerEntries);
        }

        // DELETE: api/CustomerEntries/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CustomerEntries>> DeleteCustomerEntries(int id)
        {
            var customerEntries = await _context.CustomerEntries.FindAsync(id);
            if (customerEntries == null)
            {
                return NotFound();
            }

            _context.CustomerEntries.Remove(customerEntries);
            await _context.SaveChangesAsync();

            return customerEntries;
        }

        private bool CustomerEntriesExists(int id)
        {
            return _context.CustomerEntries.Any(e => e.Id == id);
        }
    }
}
