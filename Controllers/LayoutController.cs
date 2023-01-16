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
    public class LayoutController : ControllerBase
    {
        private readonly EntitiesDbContext _context;
        public LayoutController(EntitiesDbContext context) => _context = context;

        [HttpGet("Layouts")]
        [Authorize]
        public async Task<IEnumerable<Layout>> GetLayouts() => await _context.Layouts.ToListAsync();

        [HttpGet("Layouts/{id}")]
        [ProducesResponseType(typeof(Layout), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        public async Task<IActionResult> GetLayoutById(int id)
        {
            var layout = await _context.Layouts.FindAsync(id);
            int userId = Convert.ToInt32(HttpContext.User.FindFirst("UserId")?.Value);
            var user = await _context.Users.FindAsync(userId);
            if(user == null || layout == null)
            {
                return NotFound();
            }
            if (user.TenantId != layout.TenantId)
            { 
                return Unauthorized(); 
            }
            return layout == null ? NotFound() : Ok(layout);
        }
        /// <summary>
        /// Creates a new layout
        /// </summary>
        /// <param name="layout">Object of type layout</param>
        /// <returns>The layout created</returns>
        [HttpPost("Layouts")]
        [ProducesResponseType(typeof(Layout), StatusCodes.Status201Created)]
        [Authorize("admin:True")]
        public async Task<IActionResult> Create(Layout layout)
        {
            await _context.Layouts.AddAsync(layout);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLayoutById), new { id = layout.Id }, layout);
        }

        /// <summary>
        /// Assigns a tenant to a layout based on ids
        /// </summary>
        /// <param name="layoutId"></param>
        /// <param name="tenantId"></param>
        /// <returns>The updated Layout</returns>
        [HttpPut("Layouts/{layoutId}/Assign/{tenantId}")]
        [ProducesResponseType(typeof(Layout), StatusCodes.Status201Created)]
        [Authorize("admin:True")]
        public async Task<IActionResult> AddLayoutToTenant(int layoutId, int tenantId)
        {
            var layout = await _context.Layouts.FindAsync(layoutId);
            var tenant = await _context.Tenants.FindAsync(tenantId);
            if (layout == null || tenant == null) return NotFound();
            layout.TenantId = tenantId;
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLayoutById), new { id = layout.Id }, layout);
        }
        /// <summary>
        /// Returns all rooms that belong to a specific Layout
        /// </summary>
        /// <param name="layoutId"></param>
        /// <returns></returns>

        [HttpGet("Layouts/{layoutId}/Rooms")]
        [ProducesResponseType(typeof(Room), StatusCodes.Status200OK)]
        [Authorize]
        public async Task<IEnumerable<Room>> GetRoomsFromLayoutId(int layoutId) => 
            await _context.Rooms.Where(room => room.LayoutId == layoutId).ToListAsync();

        /// <summary>
        /// Returns all images that belong to a specific Layout
        /// </summary>
        /// <param name="layoutId"></param>
        /// <returns></returns>

        [HttpGet("Layouts/{layoutId}/images")]
        [ProducesResponseType(typeof(Image), StatusCodes.Status200OK)]
        [Authorize]
        public async Task<IEnumerable<Image>> GetImagesFromLayoutId(int layoutId) =>
            await _context.Images.Where(image => image.LayoutId == layoutId).ToListAsync();
    }
}
