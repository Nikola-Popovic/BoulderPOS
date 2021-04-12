using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BoulderPOS.API.Models;
using BoulderPOS.API.Services;

namespace BoulderPOS.API.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class ProductCategoriesController : ControllerBase
    {
        private readonly IProductCategoryService _categoryService;

        public ProductCategoriesController(IProductCategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: api/categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductCategory>>> GetProductCategories()
        {
            return Ok(await _categoryService.GetProductCategories());
        }

        // GET: api/categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductCategory>> GetProductCategory(int id)
        {
            var category = await _categoryService.GetProductCategory(id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        // GET: api/categories/5
        [HttpGet("{id}/products")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategory(int id)
        {
            var products = await _categoryService.GetProductsByCategory(id);

            if (products == null)
            {
                return NotFound();
            }

            return Ok(products);
        }

        // PUT: api/categories/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductCategory(int id, ProductCategory productCategory)
        {
            if (id != productCategory.Id)
            {
                return BadRequest();
            }

            var updated = await _categoryService.UpdateProductCategory(id, productCategory);

            if (updated == null)
            {
                return NotFound();
            }

            return Ok(updated);
        }

        // PUT: api/categories/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("updateOrder")]
        public async Task<IActionResult> UpdateCategoriesOrder(ProductCategory[] productCategories)
        {
            await _categoryService.UpdateProductCategories(productCategories);
            return NoContent();
        }

        // POST: api/categories
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ProductCategory>> PostProductCategory(ProductCategory productCategory)
        {
            var created = await _categoryService.CreateProductCategory(productCategory);

            return CreatedAtAction("GetProductCategory", new { id = created.Id }, created);
        }

        // DELETE: api/categories/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProductCategory(int id)
        {
            await _categoryService.DeleteProductCategory(id);

            return NoContent();
        }
        
    }
}
