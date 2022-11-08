using Locus.Data;
using Locus.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Locus.Controllers
{
    [Route("/")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly EntitiesDbContext _context;
        public RoomController(EntitiesDbContext context) => _context = context;

        [HttpGet("Rooms")]
        public async Task<IEnumerable<Room>> GetRooms() => await _context.Rooms.ToListAsync();

        [HttpGet("Rooms/id")]
        [ProducesResponseType(typeof(Room), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRoomById(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            return room == null ? NotFound() : Ok(room);
        }
        /// <summary>
        /// Creates a new room
        /// </summary>
        /// <param name="room">Object of type room</param>
        /// <returns>The room created</returns>
        [HttpPost("Rooms")]
        [ProducesResponseType(typeof(Room), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(Room room)
        {
            await _context.Rooms.AddAsync(room);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRoomById), new { id = room.Id }, room);
        }
    }
}
