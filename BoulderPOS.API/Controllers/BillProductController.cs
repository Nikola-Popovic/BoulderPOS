using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BoulderPOS.API.Models;
using BoulderPOS.API.Services;

namespace BoulderPOS.API.Controllers
{
    [Route("api/billProducts")]
    [ApiController]
    public class BillProductController : ControllerBase
    {
        private readonly IBillProductService _paymentService;

        public BillProductController(IBillProductService paymentService)
        {
            _paymentService = paymentService;
        }

        // GET: api/payments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BillProduct>>> GetProductPayments()
        {
            return Ok(await _paymentService.GetLatestBillProducts());
        }

        // GET: api/payments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BillProduct>> GetProductPayment(int id)
        {
            var productPayment = await _paymentService.GetBillProduct(id);

            if (productPayment == null)
            {
                return NotFound();
            }

            return productPayment;
        }

        // PUT: api/payments/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductPayment(int id, BillProduct productPayment)
        {
            if (id != productPayment.Id)
            {
                return BadRequest();
            }

            var updated = await _paymentService.UpdateBillProduct(id, productPayment);

            if (updated == null)
            {
                return NotFound();
            }

            return Ok(updated);
        }

        // POST: api/payments
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<BillProduct>> PostProductPayment(BillProduct productPayment)
        {
            var created = await _paymentService.CreateBillProduct(productPayment);

            return CreatedAtAction("GetProductPayment", new { id = created.Id }, created);
        }

        // DELETE: api/payments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BillProduct>> DeleteProductPayment(int id)
        {
            await _paymentService.DeleteBillProduct(id);

            return NoContent();
        }
    }
}
