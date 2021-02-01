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
    public class ProductPaymentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductPaymentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ProductPayments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductPayment>>> GetProductPayments()
        {
            return await _context.ProductPayments.ToListAsync();
        }

        // GET: api/ProductPayments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductPayment>> GetProductPayment(int id)
        {
            var productPayment = await _context.ProductPayments.FindAsync(id);

            if (productPayment == null)
            {
                return NotFound();
            }

            return productPayment;
        }

        // PUT: api/ProductPayments/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductPayment(int id, ProductPayment productPayment)
        {
            if (id != productPayment.Id)
            {
                return BadRequest();
            }

            _context.Entry(productPayment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductPaymentExists(id))
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

        // POST: api/ProductPayments
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ProductPayment>> PostProductPayment(ProductPayment productPayment)
        {
            _context.ProductPayments.Add(productPayment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductPayment", new { id = productPayment.Id }, productPayment);
        }

        // DELETE: api/ProductPayments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductPayment>> DeleteProductPayment(int id)
        {
            var productPayment = await _context.ProductPayments.FindAsync(id);
            if (productPayment == null)
            {
                return NotFound();
            }

            _context.ProductPayments.Remove(productPayment);
            await _context.SaveChangesAsync();

            return productPayment;
        }

        private bool ProductPaymentExists(int id)
        {
            return _context.ProductPayments.Any(e => e.Id == id);
        }
    }
}
