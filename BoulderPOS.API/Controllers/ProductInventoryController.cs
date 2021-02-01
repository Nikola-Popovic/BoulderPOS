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
    public class ProductInventoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductInventoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ProductInventory
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductInventory>>> GetProductInventory()
        {
            return await _context.ProductInventory.ToListAsync();
        }

        // GET: api/ProductInventory/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductInventory>> GetProductInventory(int id)
        {
            var productInventory = await _context.ProductInventory.FindAsync(id);

            if (productInventory == null)
            {
                return NotFound();
            }

            return productInventory;
        }

        // PUT: api/ProductInventory/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductInventory(int id, ProductInventory productInventory)
        {
            if (id != productInventory.Id)
            {
                return BadRequest();
            }

            _context.Entry(productInventory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductInventoryExists(id))
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

        // POST: api/ProductInventory
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ProductInventory>> PostProductInventory(ProductInventory productInventory)
        {
            _context.ProductInventory.Add(productInventory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductInventory", new { id = productInventory.Id }, productInventory);
        }

        // DELETE: api/ProductInventory/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductInventory>> DeleteProductInventory(int id)
        {
            var productInventory = await _context.ProductInventory.FindAsync(id);
            if (productInventory == null)
            {
                return NotFound();
            }

            _context.ProductInventory.Remove(productInventory);
            await _context.SaveChangesAsync();

            return productInventory;
        }

        private bool ProductInventoryExists(int id)
        {
            return _context.ProductInventory.Any(e => e.Id == id);
        }
    }
}
