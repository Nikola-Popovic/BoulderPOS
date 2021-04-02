using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BoulderPOS.API.Models;
using BoulderPOS.API.Services;

namespace BoulderPOS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            var customers = await _customerService.GetCustomers();
            return Ok(customers);
        }
        
        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _customerService.GetCustomer(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // GET: api/Customers/search
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomersByPhoneNumber([FromQuery] string phoneNumber)
        {
            var customers = await _customerService.GetCustomersByPhone(phoneNumber);
            return Ok(customers);
        }

        // Less efficient
        // GET: api/Customers/searchAll
        [HttpGet("searchAll")]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomersByCustomerInfo([FromQuery] string customerInfo)
        {
            var customers = await _customerService.GetCustomersByCustomerInfo(customerInfo);
            return Ok(customers);
        }


        // PUT: api/Customers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, Customer customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }

            var updatedCustomer = await _customerService.UpdateCustomer(id, customer);

            if (updatedCustomer == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Customers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            var createdCustomer = await _customerService.CreateCustomer(customer);

            return CreatedAtAction("GetCustomer", new { id = createdCustomer.Id }, createdCustomer);
        }

        // PUT: api/Customers/5/checkin
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}/checkin")]
        public async Task<ActionResult<bool>> CheckinCustomer(int id)
        {
            var checkedIn = await _customerService.CheckInCustomer(id);

            return Ok(checkedIn);
        }


        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Customer>> DeleteCustomer(int id)
        {
            await _customerService.DeleteCustomer(id);
            return NoContent();
        }
    }
}
