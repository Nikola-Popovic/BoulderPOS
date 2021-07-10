using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoulderPOS.API.Models;
using BoulderPOS.API.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BoulderPOS.API.Services
{
    public class BillService : IBillService
    {
        private readonly ApplicationDbContext _context;
        private readonly IBillProductService _billProductService;

        public BillService(ApplicationDbContext context, IBillProductService billProductService)
        {
            _context = context;
            _billProductService = billProductService;
        }

        public async Task<IEnumerable<Bill>> GetLatestBills()
        {
            var billsByDate = _context.Bills.OrderByDescending(bill => bill.ProcessedDateTime);
            return await billsByDate.ToListAsync();
        }

        public async Task<Bill> GetBill(int id)
        {
            return await _context.Bills.FindAsync(id);
        }

        public async Task<Bill> UpdateBill(int id, Bill bill)
        {
            bill.UpdatedDateTime = DateTime.Now;
            _context.Entry(bill).State = EntityState.Modified;
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BillExists(id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            return bill;
        }

        public async Task<Bill> CreateBill(Bill bill)
        {
            if (_context.BillProducts.Any())
            {
                bill.Id = await _context.Bills.MaxAsync(p => p.Id) + 1;
            }
            bill.ProcessedDateTime = DateTime.Now;
            bill.UpdatedDateTime = DateTime.Now;

            var created = _context.Bills.Add(bill);
            await _context.SaveChangesAsync();
            return created.Entity;
        }

        public async Task<Bill> DeleteBill(int id)
        {
            var bill = await _context.Bills.FindAsync(id);
            if (bill == null)
            {
                return null;
            }

            _context.Bills.Remove(bill);
            await _context.SaveChangesAsync();
            return bill;
        }

        public bool BillExists(int id)
        {
            return _context.Bills.Any(e => e.Id == id);
        }
    }
}
