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
    public class UserController : ControllerBase
    {
        private readonly EntitiesDbContext _context;
        public UserController(EntitiesDbContext context) => _context = context;

        [HttpGet("Users")]
        public async Task<IEnumerable<User>> GetUsers() => await _context.Users.ToListAsync();

        [HttpGet("Users/id")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _context.Users.FindAsync(id);
            return user == null ? NotFound() : Ok(user);
        }
        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <param name="user">Object of type User</param>
        /// <returns>The user created</returns>
        [HttpPost("Users")]
        [ProducesResponseType(typeof(User), StatusCodes.Status201Created)]
        [Authorize("admin:true")]
        public async Task<IActionResult> Create(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }

        /// <summary>
        /// Assigns a tenant to a user based on ids
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="tenantId"></param>
        /// <returns>The updated User</returns>
        [HttpPut("Users/{userId}/Assign/{tenantId}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status201Created)]
        [Authorize("admin:true")]
        public async Task<IActionResult> AddUserToTenant(int userId, int tenantId)
        {
            var user = await _context.Users.FindAsync(userId);
            var tenant = await _context.Tenants.FindAsync(tenantId);
            if(user == null || tenant == null) return NotFound();
            user.TenantId = tenantId;
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }

    }
}