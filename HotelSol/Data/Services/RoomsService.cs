using HotelSol.Data;
using HotelSol.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelSol.Data.Services
{
    public class RoomsService
    {
        private readonly HotelSolDbContext _context;

        public RoomsService(HotelSolDbContext context)
        {
            _context = context;
        }

        // Agregar una nueva habitación
        public async Task AddRoom(Rooms room)
        {
            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();
        }

        // Obtener todas las habitaciones
        public List<Rooms> GetAllRooms()
        {
            return _context.Rooms.ToList();
        }

        // Obtener una habitación por su ID
        public Rooms GetRoomById(int id)
        {
            return _context.Rooms.FirstOrDefault(r => r.RoomID == id);
        }

        // Actualizar una habitación
        public async Task UpdateRoom(Rooms room)
        {
            _context.Rooms.Update(room);
            await _context.SaveChangesAsync();
        }

        // Eliminar una habitación
        public async Task DeleteRoom(int id)
        {
            var room = _context.Rooms.FirstOrDefault(r => r.RoomID == id);
            if (room != null)
            {
                _context.Rooms.Remove(room);
                await _context.SaveChangesAsync();
            }
        }
    }
}
