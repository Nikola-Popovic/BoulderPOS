using System.Collections.Generic;
using System.Threading.Tasks;
using BoulderPOS.API.Models;

namespace BoulderPOS.API.Services
{
    public interface IBillService
    {
        Task<IEnumerable<Bill>> GetLatestBills();
        Task<Bill> GetBill(int id);
        Task<Bill> UpdateBill(int id, Bill bill);
        Task<Bill> CreateBill(Bill bill);
        Task<Bill> DeleteBill(int id);
        bool BillExists(int id);
    }
}
