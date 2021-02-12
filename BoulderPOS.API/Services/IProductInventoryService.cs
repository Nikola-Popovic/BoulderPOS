using System.Collections.Generic;
using System.Threading.Tasks;
using BoulderPOS.API.Models;

namespace BoulderPOS.API.Services
{
    public interface IProductInventoryService
    {
        Task<IEnumerable<ProductInventory>> GetProductInventory();
        Task<ProductInventory> GetProductInventory(int id);
        Task<ProductInventory> UpdateProductInventory(int id, ProductInventory productInventory);
        Task<ProductInventory> CreateProductInventory(ProductInventory product);
        Task<ProductInventory> DeleteProductInventory(int id);
        bool ProductInventoryExists(int id);
    }
}
