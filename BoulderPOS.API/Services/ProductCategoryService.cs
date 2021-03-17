using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using BoulderPOS.API.Models;
using BoulderPOS.API.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BoulderPOS.API.Services
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly ApplicationDbContext _context;

        public ProductCategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductCategory>> GetProductCategories()
        {
            return await _context.ProductCategories.ToListAsync();
        }

        public async Task<ProductCategory> GetProductCategory(int id)
        {
            return await _context.ProductCategories.FindAsync(id);
        }

        public Task<IEnumerable<Product>> GetProductsByCategory(int id)
        {
            var products = _context.Products.Where(product => product.CategoryId == id).AsEnumerable();
            return Task.FromResult(products);
        }

        public async Task<ProductCategory> UpdateProductCategory(int id, ProductCategory productCategory)
        {
            _context.Entry(productCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductCategoryExists(id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            return productCategory;
        }

        public async Task<ProductCategory> CreateProductCategory(ProductCategory productCategory)
        {
            productCategory.Id = (_context.ProductCategories.Max(c => c.Id) + 1);
            _context.ProductCategories.Add(productCategory);
            await _context.SaveChangesAsync();
            return productCategory;
        }

        public async Task<ProductCategory> DeleteProductCategory(int id)
        {
            var productCategory = await _context.ProductCategories.FindAsync(id);
            if(productCategory == null) {
                return null;
            }
            _context.ProductCategories.Remove(productCategory);
            await _context.SaveChangesAsync();
            return productCategory;
        }

        public bool ProductCategoryExists(int id)
        {
            return _context.ProductCategories.Any(e => e.Id == id);
        }
    }
}
