﻿using System.Collections.Generic;
using System.Threading.Tasks;
using BoulderPOS.API.Models;

namespace BoulderPOS.API.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAvailableProducts();
        Task<Product> GetProduct(int id);
        Task<Product> UpdateProduct(int id, Product product);
        Task DeleteProduct(int id);
        Task<Product> CreateProduct(Product product, bool createInventory);
        bool ProductExists(int id);
    }
}
