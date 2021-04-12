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
        private readonly ICustomerSubscriptionService _subscriptionService;
        private readonly ICustomerEntriesService _entriesService;
        private readonly IProductCategoryService _categoryService;

        public ProductPaymentService(ApplicationDbContext context, 
            ICustomerEntriesService entriesService, 
            ICustomerSubscriptionService subscriptionService,
            IProductCategoryService categoryService)
        {
            _subscriptionService = subscriptionService;
            _entriesService = entriesService;
            _categoryService = categoryService;
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

            // Todo : If is refunded and entries or subscription. Remove subscription or entries.
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

            var created = _context.ProductPayments.Add(productPayment);
            await _context.SaveChangesAsync();
            return created.Entity;
        }

        private async Task<bool> IfProductIsSubscriptionAddTime(ProductPayment productPayment, Product product, ProductCategory productCategory)
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

        private async Task<bool> IfProductIsEntriesAddEntries(ProductPayment productPayment, Product product, ProductCategory productCategory)
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
