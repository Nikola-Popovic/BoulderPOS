using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BoulderPOS.API.Models;
using BoulderPOS.API.Services;

namespace BoulderPOS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductInventoryController : ControllerBase
    {
        private readonly IProductInventoryService _inventoryService;

        public ProductInventoryController(IProductInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }
        

        // GET: api/ProductInventory
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductInventory>>> GetProductInventory()
        {
            return Ok(await _inventoryService.GetProductInventory());
        }

        // GET: api/ProductInventory/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductInventory>> GetProductInventory(int id)
        {
            var productInventory = await _inventoryService.GetProductInventory(id);

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

            var updated = await _inventoryService.UpdateProductInventory(id, productInventory);
            if (updated == null)
            {
                return NotFound();
            }

            return Ok(updated);
        }

        // POST: api/ProductInventory
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ProductInventory>> PostProductInventory(ProductInventory productInventory)
        {
            var created = await _inventoryService.CreateProductInventory(productInventory);

            return CreatedAtAction("GetProductInventory", new { id = created.Id }, created);
        }

        // DELETE: api/ProductInventory/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductInventory>> DeleteProductInventory(int id)
        {
            await _inventoryService.DeleteProductInventory(id);
            return NoContent();
        }
    }
}
