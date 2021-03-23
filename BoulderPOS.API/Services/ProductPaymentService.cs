using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using BoulderPOS.API.Models;
using BoulderPOS.API.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BoulderPOS.API.Services
{
    public class ProductPaymentService : IProductPaymentService
    {
        private readonly ApplicationDbContext _context;

        public ProductPaymentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductPayment>> GetLatestProductPayments()
        {
            var paymentsByDate = _context.ProductPayments.OrderByDescending(payment => payment.ProcessedDateTime);
            return await paymentsByDate.ToListAsync();
        }

        public async Task<ProductPayment> GetProductPayment(int id)
        {
            return await _context.ProductPayments.FindAsync(id);
        }

        public async Task<ProductPayment> UpdateProductPayment(int id, ProductPayment productPayment)
        {
            productPayment.UpdatedDateTime = DateTime.Now;
            _context.Entry(productPayment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductPaymentExists(id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            return productPayment;
        }

        public async Task<ProductPayment> CreateProductPayment(ProductPayment productPayment)
        {
            if (_context.ProductPayments.Any())
            {
                productPayment.Id = await _context.ProductPayments.MaxAsync(p => p.Id) + 1;
            }
            productPayment.ProcessedDateTime = DateTime.Now;
            productPayment.UpdatedDateTime = DateTime.Now;
            var created = _context.ProductPayments.Add(productPayment);
            await _context.SaveChangesAsync();
            return created.Entity;
        }

        public async Task<ProductPayment> DeleteProductPayment(int id)
        {
            var productPayment = await _context.ProductPayments.FindAsync(id);
            if (productPayment == null)
            {
                return null;
            }

            _context.ProductPayments.Remove(productPayment);
            await _context.SaveChangesAsync();
            return productPayment;
        }

        public bool ProductPaymentExists(int id)
        {
            return _context.ProductPayments.Any(e => e.Id == id);
        }
    }
}
