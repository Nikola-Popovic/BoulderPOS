using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BoulderPOS.API.Models;
using BoulderPOS.API.Services;

namespace BoulderPOS.API.Controllers
{
    [Route("api/entries")]
    [ApiController]
    public class CustomerEntriesController : ControllerBase
    {
        private readonly ICustomerEntriesService _entriesService;

        public CustomerEntriesController(ICustomerEntriesService entriesService)
        {
            _entriesService = entriesService;
        }

        // GET: api/entries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerEntries>> GetCustomerEntries(int customerId)
        {
            var entries = await _entriesService.GetCustomerEntries(customerId);

            if (entries == null)
            {
                return NotFound();
            }

            return entries;
        }

        // PUT: api/entries/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomerEntries(int customerId, CustomerEntries customerEntries)
        {
            if (customerId != customerEntries.Id)
            {
                return BadRequest();
            }

            var entries = await _entriesService.UpdateCustomerEntries(customerId, customerEntries);

            if (entries == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/entries/5/add
        [HttpPost("{id}/add")]
        public async Task<IActionResult> AddCustomerEntries(int customerId, int quantity)
        {
            var entries = await _entriesService.AddCustomerEntries(customerId, quantity);

            if (entries == null)
            {
                return NotFound();
            }

            return Ok(entries);
        }

        // POST: api/entries/5/remove
        [HttpPost("{id}/remove")]
        public async Task<IActionResult> RemoveCustomerEntries(int customerId, int quantity)
        {
            var entries = await _entriesService.TakeCustomerEntries(customerId, quantity);

            if (entries == null)
            {
                return NotFound();
            }

            return Ok(entries);
        }
    }
}
