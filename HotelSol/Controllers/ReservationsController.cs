using Microsoft.AspNetCore.Mvc;
using HotelSol.Data;
using HotelSol.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace HotelSol.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly HotelSolDbContext _context;

        public ReservationsController(HotelSolDbContext context)
        {
            _context = context;
        }

        // GET: api/Reservations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reservations>>> GetReservations()
        {
            return await _context.Reservations
                .Select(r => new Reservations
                {
                    ReservationID = r.ReservationID,
                    GuestID = r.GuestID,
                    CheckInDate = r.CheckInDate,
                    CheckOutDate = r.CheckOutDate
                })
                .ToListAsync();
        }

        // GET: api/Reservations/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReservationById(int id)
        {
            try
            {
                // Consultamos la reserva por ID
                var reservation = await _context.Reservations
                    .AsNoTracking() // Para evitar tracking innecesario
                    .Where(r => r.ReservationID == id)
                    .Select(r => new
                    {
                        r.ReservationID,
                        r.GuestID,
                        r.CheckInDate,
                        r.CheckOutDate
                    })
                    .FirstOrDefaultAsync();

                // Verificamos si no se encontró
                if (reservation == null)
                {
                    return NotFound($"Reservation with ID {id} not found.");
                }

                // Retornamos solo las propiedades necesarias
                return Ok(reservation);
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        // POST: api/Reservations
        [HttpPost]
        public async Task<IActionResult> PostReservation([FromBody] Reservations reservation)
        {
            if (reservation == null)
            {
                return BadRequest("Invalid reservation data.");
            }

            try
            {
                // Validar si el Guest existe
                var guest = await _context.Guests.FindAsync(reservation.GuestID);
                if (guest == null)
                {
                    return BadRequest($"Guest with ID {reservation.GuestID} not found.");
                }

                // Asignar Guests automáticamente al modelo
                reservation.Guests = guest;

                // No se incluye ReservationID porque será autogenerado por la base de datos
                _context.Reservations.Add(reservation);
                await _context.SaveChangesAsync();

                // Retornar el objeto creado con el ID generado automáticamente
                return CreatedAtAction("GetReservations", new { id = reservation.ReservationID }, new
                {
                    reservation.ReservationID,
                    reservation.GuestID,
                    reservation.CheckInDate,
                    reservation.CheckOutDate
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        // PUT: api/Reservations/5
        [HttpPut]
        public async Task<IActionResult> PutReservation([FromBody] Reservations updatedReservation)
        {
            if (updatedReservation.ReservationID == 0)
            {
                return BadRequest("Reservation ID must be provided in the body.");
            }

            try
            {
                // Validar si la reservación existe
                var existingReservation = await _context.Reservations.FindAsync(updatedReservation.ReservationID);
                if (existingReservation == null)
                {
                    return NotFound($"Reservation with ID {updatedReservation.ReservationID} not found.");
                }

                // Actualizar los valores directamente
                existingReservation.GuestID = updatedReservation.GuestID;
                existingReservation.CheckInDate = updatedReservation.CheckInDate;
                existingReservation.CheckOutDate = updatedReservation.CheckOutDate;

                // Guardar los cambios
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/Reservations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
