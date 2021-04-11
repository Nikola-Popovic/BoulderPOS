using System.Collections.Generic;
using System.Threading.Tasks;
using BoulderPOS.API.Models;

namespace BoulderPOS.API.Services
{
    public interface IProductCategoryService
    {
        Task<IEnumerable<ProductCategory>> GetProductCategories();
        Task<ProductCategory> GetProductCategory(int id);
        Task<IEnumerable<Product>> GetProductsByCategory(int id);
        Task<ProductCategory> UpdateProductCategory(int id, ProductCategory productCategory);
        Task UpdateProductCategories(ProductCategory[] productCategories);
        Task<ProductCategory> CreateProductCategory(ProductCategory productCategory);
        Task<ProductCategory> DeleteProductCategory(int id);
        bool ProductCategoryExists(int id);
    }
}
