using Microsoft.AspNetCore.Mvc;
using HotelSol.Data;
using HotelSol.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelSol.Data.ViewModels;

namespace HotelSol.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestsController : ControllerBase
    {
        private readonly HotelSolDbContext _context;

        public GuestsController(HotelSolDbContext context)
        {
            _context = context;
        }

        // GET: api/Guests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Guests>>> GetGuests()
        {
            return await _context.Guests.ToListAsync();
        }

        // GET: api/Guests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Guests>> GetGuest(int id)
        {
            var guest = await _context.Guests.FindAsync(id);

            if (guest == null)
            {
                return NotFound();
            }

            return guest;
        }

        // POST: api/Guests
        [HttpPost]
        public async Task<IActionResult> PostGuest(Guests guest)
        {
            if (guest == null)
            {
                return BadRequest();
            }

            // No es necesario asignar el GuestID, ya que es una columna de identidad.
            _context.Guests.Add(guest);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetGuest), new { id = guest.GuestID }, guest);
        }


        // PUT: api/Guests/5
        [HttpPut]
        public async Task<IActionResult> PutGuest(Guests guest)
        {
            var existingGuest = await _context.Guests.FindAsync(guest.GuestID);

            if (existingGuest == null)
            {
                return NotFound("El huésped no existe.");
            }

            // Actualiza los campos relevantes
            existingGuest.FullName = guest.FullName;
            existingGuest.ContactInfo = guest.ContactInfo;
            // Agrega más campos si es necesario

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return Ok("Información actualizada correctamente.");
        }



        // DELETE: api/Guests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGuest(int id)
        {
            var guest = await _context.Guests.FindAsync(id);
            if (guest == null)
            {
                return NotFound();
            }

            _context.Guests.Remove(guest);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
