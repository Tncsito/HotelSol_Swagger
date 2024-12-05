using Microsoft.AspNetCore.Mvc;
using HotelSol.Data;
using HotelSol.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelSol.Data.ViewModels;
using System;

namespace HotelSol.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly HotelSolDbContext _context;

        public PaymentsController(HotelSolDbContext context)
        {
            _context = context;
        }

        // GET: api/Payments
        [HttpGet]
        public async Task<IActionResult> GetPayments()
        {
            var payments = await _context.Payments.ToListAsync();

            if (payments == null || !payments.Any())
            {
                return NotFound("No se encontraron registros en la tabla de pagos.");
            }

            return Ok(payments);
        }


        // GET: api/Payments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Payments>> GetPayment(int id)
        {
            var payment = await _context.Payments.Include(p => p.Reservation)
                                                 .FirstOrDefaultAsync(p => p.PaymentID == id);

            if (payment == null)
            {
                return NotFound();
            }

            return payment;
        }

        // POST: api/Payments
        [HttpPost]
        public async Task<IActionResult> PostPayment([FromBody] Payments payment)
        {
            if (payment == null)
            {
                return BadRequest("Invalid payment data.");
            }

            try
            {
                var reservation = await _context.Reservations.FindAsync(payment.ReservationID);
                if (reservation == null)
                {
                    return BadRequest($"Reservation with ID {payment.ReservationID} not found.");
                }

                payment.PaymentDate = DateTime.UtcNow;

                _context.Payments.Add(payment);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetPayment", new { id = payment.PaymentID }, payment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutPayment(int id, [FromBody] Payments payment)
        {
            if (payment == null || payment.PaymentID != id)
            {
                return BadRequest("Payment data is invalid.");
            }

            try
            {
                var existingPayment = await _context.Payments.FindAsync(id);
                if (existingPayment == null)
                {
                    return NotFound($"Payment with ID {id} not found.");
                }

                existingPayment.AmountPaid = payment.AmountPaid;
                existingPayment.PaymentMethod = payment.PaymentMethod;
                existingPayment.PaymentDate = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        // DELETE: api/Payments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }

            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
