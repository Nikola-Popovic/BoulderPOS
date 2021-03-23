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
        [HttpGet("{productId}")]
        public async Task<ActionResult<ProductInventory>> GetProductInventory(int productId)
        {
            var productInventory = await _inventoryService.GetProductInventory(productId);

            if (productInventory == null)
            {
                return NotFound();
            }

            return productInventory;
        }

        // PUT: api/ProductInventory/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{productId}")]
        public async Task<ActionResult<ProductInventory>> PutProductInventory(int productId, ProductInventory productInventory)
        {
            if (productId != productInventory.ProductId)
            {
                return BadRequest();
            }

            var updated = await _inventoryService.UpdateProductInventory(productId, productInventory);
            if (updated == null)
            {
                return NotFound();
            }

            return Ok(updated);
        }

        // POST: api/ProductInventory/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ProductInventory>> CreateProductInventory(ProductInventory productInventory)
        {
            var created = await _inventoryService.CreateProductInventory(productInventory);

            return CreatedAtAction("GetProductInventory", new { productId = created.ProductId }, created);
        }
    }
}
