using Locus.Data;
using Locus.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Locus.Controllers
{
    [Route("/")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly EntitiesDbContext _context;
        public ReservationController(EntitiesDbContext context) => _context = context;

        [HttpGet("Reservations")]
        [Authorize]
        public async Task<IEnumerable<Reservation>> GetReservations() => await _context.Reservations.ToListAsync();

        [HttpGet("Reservations/{id}")]
        [ProducesResponseType(typeof(Reservation), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        public async Task<IActionResult> GetReservationById(int id)
        {
            var Reservation = await _context.Reservations.FindAsync(id);
            return Reservation == null ? NotFound() : Ok(Reservation);
        }
        /// <summary>
        /// Creates a new Reservation
        /// </summary>
        /// <param name="Reservation">Object of type Reservation</param>
        /// <returns>The Reservation created</returns>
        [HttpPost("Reservations")]
        [ProducesResponseType(typeof(Reservation), StatusCodes.Status201Created)]
        [Authorize]
        public async Task<IActionResult> Create(Reservation Reservation)
        {
            await _context.Reservations.AddAsync(Reservation);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetReservationById), new { id = Reservation.Id }, Reservation);
        }

        /// <summary>
        /// Assigns a room to a reservation based on ids
        /// </summary>
        /// <param name="reservationId"></param>
        /// <param name="roomId"></param>
        /// <returns>The updated Rservation</returns>
        [HttpPut("Reservations/{reservationId}/Assign/{roomId}")]
        [ProducesResponseType(typeof(Reservation), StatusCodes.Status201Created)]
        [Authorize]
        public async Task<IActionResult> AddReservationToRoom(int reservationId, int roomId)
        {
            var reservation = await _context.Reservations.FindAsync(reservationId);
            var room = await _context.Rooms.FindAsync(roomId);
            if (reservation == null || room == null) return NotFound();
            reservation.RoomId = roomId;
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetReservationById), new { id = reservation.Id }, reservation);
        }
    }
}
