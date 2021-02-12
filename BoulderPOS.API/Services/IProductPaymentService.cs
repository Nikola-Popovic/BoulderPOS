using System.Collections.Generic;
using System.Threading.Tasks;
using BoulderPOS.API.Models;

namespace BoulderPOS.API.Services
{
    public interface IProductPaymentService
    {
        Task<IEnumerable<ProductPayment>> GetProductPayments();
        Task<ProductPayment> GetProductPayment(int id);
        Task<ProductPayment> UpdateProductPayment(int id, ProductPayment productPayment);
        Task<ProductPayment> CreateProductPayment(ProductPayment productPayment);
        Task<ProductPayment> DeleteProductPayment(int id);
        bool ProductPaymentExists(int id);
    }
}
