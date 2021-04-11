using System;
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
            var categoryByOrder = _context.ProductCategories.OrderBy(category => category.Order);
            return await categoryByOrder.ToListAsync();
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

        public async Task UpdateProductCategories(ProductCategory[] productCategories)
        {
            
            foreach (var productCategory in productCategories)
            {
                await UpdateProductCategory(productCategory.Id, productCategory);
            }
        }

        public async Task<ProductCategory> CreateProductCategory(ProductCategory productCategory)
        {
            if (_context.ProductCategories.Any())
            {
                productCategory.Id = await _context.ProductCategories.MaxAsync(p => p.Id) + 1;
                productCategory.Order = await _context.ProductCategories.MaxAsync(p => p.Order) + 1;
            }
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
            // See if this is completely necessary
            if (productCategory.IsEntries || productCategory.IsSubscription )
            {
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
