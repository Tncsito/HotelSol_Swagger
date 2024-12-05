using HotelSol.Data;
using HotelSol.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelSol.Data.Services
{
    public class PaymentsService
    {
        private readonly HotelSolDbContext _context;

        public PaymentsService(HotelSolDbContext context)
        {
            _context = context;
        }

        // Agregar un nuevo pago
        public async Task AddPayment(Payments payment)
        {
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();
        }

        // Obtener todos los pagos
        public List<Payments> GetAllPayments()
        {
            return _context.Payments.ToList();
        }

        // Obtener un pago por su ID
        public Payments GetPaymentById(int id)
        {
            return _context.Payments.FirstOrDefault(p => p.PaymentID == id);
        }

        // Actualizar un pago
        public async Task UpdatePayment(Payments payment)
        {
            _context.Payments.Update(payment);
            await _context.SaveChangesAsync();
        }

        // Eliminar un pago
        public async Task DeletePayment(int id)
        {
            var payment = _context.Payments.FirstOrDefault(p => p.PaymentID == id);
            if (payment != null)
            {
                _context.Payments.Remove(payment);
                await _context.SaveChangesAsync();
            }
        }
    }
}
