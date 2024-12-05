using HotelSol.Data;
using HotelSol.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelSol.Data.Services
{
    public class GuestsService
    {
        private readonly HotelSolDbContext _context;

        public GuestsService(HotelSolDbContext context)
        {
            _context = context;
        }

        // Agregar un nuevo huésped
        public async Task AddGuest(Guests guest)
        {
            _context.Guests.Add(guest);
            await _context.SaveChangesAsync();
        }

        // Obtener todos los huéspedes
        public List<Guests> GetAllGuests()
        {
            return _context.Guests.ToList();
        }

        // Obtener un huésped por su ID
        public Guests GetGuestById(int id)
        {
            return _context.Guests.FirstOrDefault(g => g.GuestID == id);
        }

        // Actualizar un huésped
        public async Task UpdateGuest(Guests guest)
        {
            _context.Guests.Update(guest);
            await _context.SaveChangesAsync();
        }

        // Eliminar un huésped
        public async Task DeleteGuest(int id)
        {
            var guest = _context.Guests.FirstOrDefault(g => g.GuestID == id);
            if (guest != null)
            {
                _context.Guests.Remove(guest);
                await _context.SaveChangesAsync();
            }
        }
    }
}
