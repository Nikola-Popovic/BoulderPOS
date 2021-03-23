using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using BoulderPOS.API.Models;
using BoulderPOS.API.Persistence;

namespace BoulderPOS.API.Services
{
    public class ProductInventoryService : IProductInventoryService
    {
        private readonly ApplicationDbContext _context;

        public ProductInventoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductInventory>> GetProductInventory()
        {
            return await _context.ProductInventory.ToListAsync();
        }

        public async Task<ProductInventory> GetProductInventory(int productId)
        {
            var inventory = await _context.ProductInventory.FirstAsync(inv => inv.ProductId == productId);
            return inventory;
        }

        public async Task<ProductInventory> UpdateProductInventory(int id, ProductInventory productInventory)
        {
            _context.Entry(productInventory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductInventoryExists(id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            return productInventory;
        }

        public async Task<ProductInventory> CreateProductInventory(ProductInventory inventory)
        {
            var created = _context.ProductInventory.Add(inventory);
            await _context.SaveChangesAsync();
            return created.Entity;
        }

        public async Task<ProductInventory> DeleteProductInventory(int productId)
        {
            var productInventory = await GetProductInventory(productId);
            if (productInventory == null)
            {
                return null;
            }

            _context.ProductInventory.Remove(productInventory);
            await _context.SaveChangesAsync();

            return productInventory;
        }

        public bool ProductInventoryExists(int productId)
        {
            return _context.ProductInventory.Any(e => e.ProductId == productId);
        }
    }
}
