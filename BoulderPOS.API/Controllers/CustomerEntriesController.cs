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
        [HttpGet("{customerId}")]
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
        [HttpPut("{customerId}")]
        public async Task<IActionResult> PutCustomerEntries(int customerId, CustomerEntries customerEntries)
        {
            if (customerId != customerEntries.CustomerId)
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

        // PUT: api/entries/5/add
        [HttpPut("{customerId}/add")]
        public async Task<ActionResult<CustomerEntries>> AddCustomerEntries(int customerId,[FromQuery] int quantity)
        {
            var entries = await _entriesService.AddCustomerEntries(customerId, quantity);

            if (entries == null)
            {
                return NotFound();
            }

            return Ok(entries);
        }

        // PUT: api/entries/5/remove
        [HttpPut("{customerId}/remove")]
        public async Task<ActionResult<CustomerEntries>> RemoveCustomerEntries(int customerId,[FromQuery] int quantity)
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
