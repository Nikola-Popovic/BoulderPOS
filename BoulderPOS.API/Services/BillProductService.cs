using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using BoulderPOS.API.Models;
using BoulderPOS.API.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BoulderPOS.API.Services
{
    public class BillProductService : IBillProductService
    {
        private readonly ApplicationDbContext _context;
        private readonly ICustomerSubscriptionService _subscriptionService;
        private readonly ICustomerEntriesService _entriesService;
        private readonly IProductCategoryService _categoryService;

        public BillProductService(ApplicationDbContext context, 
            ICustomerEntriesService entriesService, 
            ICustomerSubscriptionService subscriptionService,
            IProductCategoryService categoryService)
        {
            _subscriptionService = subscriptionService;
            _entriesService = entriesService;
            _categoryService = categoryService;
            _context = context;
        }

        public async Task<IEnumerable<BillProduct>> GetLatestBillProducts()
        {
            var paymentsByDate = _context.BillProducts.OrderByDescending(payment => payment.ProcessedDateTime);
            return await paymentsByDate.ToListAsync();
        }

        public async Task<BillProduct> GetBillProduct(int id)
        {
            return await _context.BillProducts.FindAsync(id);
        }

        public async Task<BillProduct> UpdateBillProduct(int id, BillProduct productPayment)
        {
            productPayment.UpdatedDateTime = DateTime.Now;
            _context.Entry(productPayment).State = EntityState.Modified;

            // Todo : If is refunded and entries or subscription. Remove subscription or entries.
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BillProductExists(id))
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

        public async Task<BillProduct> CreateBillProduct(BillProduct productPayment)
        {
            if (_context.BillProducts.Any())
            {
                productPayment.Id = await _context.BillProducts.MaxAsync(p => p.Id) + 1;
            }
            productPayment.ProcessedDateTime = DateTime.Now;
            productPayment.UpdatedDateTime = DateTime.Now;
            
            var product = productPayment.Product;
            if (product != null)
            {
                productPayment.ProductId = product.Id;
                // Do not save the inner child
                productPayment.Product = null;
            }
            else
            {
                product = await _context.Products.FindAsync(productPayment.ProductId);
            }
            var productCategory = product.Category ?? await _categoryService.GetProductCategory(product.CategoryId);
            if (await IfProductIsEntriesAddEntries(productPayment, product, productCategory)) return null;
            if (await IfProductIsSubscriptionAddTime(productPayment, product, productCategory)) return null;

            var created = _context.BillProducts.Add(productPayment);
            await _context.SaveChangesAsync();
            return created.Entity;
        }

        private async Task<bool> IfProductIsSubscriptionAddTime(BillProduct productPayment, Product product, ProductCategory productCategory)
        {
            if (productCategory?.IsSubscription == true)
            {
                if (productPayment.CustomerId == null)
                {
                    return true;
                }

                // Customers would very rarely buy multiple subscriptions
                var subscriptionTimeToAdd = productPayment.Quantity * (int)product.Quantity;
                await _subscriptionService.AddCustomerSubscription((int) productPayment.CustomerId,
                    subscriptionTimeToAdd);
            }

            return false;
        }

        private async Task<bool> IfProductIsEntriesAddEntries(BillProduct productPayment, Product product, ProductCategory productCategory)
        {
            if (productCategory?.IsEntries == true)
            {
                if (productPayment.CustomerId == null)
                {
                    return true;
                }

                var entriesToAdd = productPayment.Quantity * product.Quantity;
                await _entriesService.AddCustomerEntries((int) productPayment.CustomerId, entriesToAdd);
            }

            return false;
        }
        

        public async Task<BillProduct> DeleteBillProduct(int id)
        {
            var productPayment = await _context.BillProducts.FindAsync(id);
            if (productPayment == null)
            {
                return null;
            }

            _context.BillProducts.Remove(productPayment);
            await _context.SaveChangesAsync();
            return productPayment;
        }

        public bool BillProductExists(int id)
        {
            return _context.BillProducts.Any(e => e.Id == id);
        }
    }
}
