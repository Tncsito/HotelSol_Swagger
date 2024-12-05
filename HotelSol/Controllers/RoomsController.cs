using Microsoft.AspNetCore.Mvc;
using HotelSol.Data;
using HotelSol.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;

namespace HotelSol.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly HotelSolDbContext _context;

        public RoomsController(HotelSolDbContext context)
        {
            _context = context;
        }

        // GET: api/Rooms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rooms>>> GetRooms()
        {
            try
            {
                var rooms = await _context.Rooms.ToListAsync();
                return Ok(rooms.Select(r => new
                {
                    r.RoomID,
                    r.RoomNumber,
                    r.RoomType,
                    r.PricePerNight // Asegúrate de usar el tipo correcto aquí
                }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        // GET: api/Rooms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Rooms>> GetRoom(int id)
        {
            var room = await _context.Rooms.FindAsync(id);

            if (room == null)
            {
                return NotFound();
            }

            return room;
        }

        // POST: api/Rooms
        [HttpPost]
        public async Task<IActionResult> PostRoom([FromBody] Rooms room)
        {
            if (room == null)
            {
                return BadRequest("Invalid room data.");
            }

            try
            {
                // Remover cualquier asociación con ReservationRooms para asegurar que no se guarden en este contexto
                room.ReservationRooms = null;

                // Agregar el nuevo room al contexto
                _context.Rooms.Add(room);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetRoom", new { id = room.RoomID }, room);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        // PUT: api/Rooms/5
        [HttpPut]
        public async Task<IActionResult> PutRoom([FromBody] Rooms updatedRoom)
        {
            if (updatedRoom.RoomID == 0)
            {
                return BadRequest("Room ID must be provided in the body.");
            }

            try
            {
                // Validar si la habitación existe
                var existingRoom = await _context.Rooms.FindAsync(updatedRoom.RoomID);
                if (existingRoom == null)
                {
                    return NotFound($"Room with ID {updatedRoom.RoomID} not found.");
                }

                // Actualizar los valores directamente
                existingRoom.RoomNumber = updatedRoom.RoomNumber;
                existingRoom.RoomType = updatedRoom.RoomType;
                existingRoom.PricePerNight = updatedRoom.PricePerNight;

                // Guardar los cambios
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/Rooms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }

            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
