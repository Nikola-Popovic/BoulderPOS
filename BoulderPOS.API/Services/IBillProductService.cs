using System.Collections.Generic;
using System.Threading.Tasks;
using BoulderPOS.API.Models;

namespace BoulderPOS.API.Services
{
    public interface IBillProductService
    {
        Task<IEnumerable<BillProduct>> GetLatestBillProducts();
        Task<BillProduct> GetBillProduct(int id);
        Task<BillProduct> UpdateBillProduct(int id, BillProduct productPayment);
        Task<BillProduct> CreateBillProduct(BillProduct productPayment);
        Task<BillProduct> DeleteBillProduct(int id);
        bool BillProductExists(int id);
    }
}
