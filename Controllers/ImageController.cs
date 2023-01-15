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
    public class ImageController : ControllerBase
    {
        private readonly EntitiesDbContext _context;
        public ImageController(EntitiesDbContext context) => _context = context;

        [HttpGet("Images")]
        [Authorize]
        public async Task<IEnumerable<Image>> GetImages() => await _context.Images.ToListAsync();

        [HttpGet("Images/id")]
        [ProducesResponseType(typeof(Image), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        public async Task<IActionResult> GetImageById(int id)
        {
            var image = await _context.Images.FindAsync(id);
            int userId = Convert.ToInt32(HttpContext.User.FindFirst("UserId")?.Value);
            var user = await _context.Users.FindAsync(userId);

            if (user == null || image == null)
            {
                return NotFound();
            }
            if (user.TenantId != image.TenantId)
            {
                return Unauthorized();
            }

            return image == null ? NotFound() : Ok(image);
        }
        /// <summary>
        /// Creates a new Image
        /// </summary>
        /// <param name="Image">Object of type Image</param>
        /// <returns>The Image created</returns>
        [HttpPost("Images")]
        [ProducesResponseType(typeof(Image), StatusCodes.Status201Created)]
        [Authorize]
        public async Task<IActionResult> Create(Image Image)
        {
            await _context.Images.AddAsync(Image);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetImageById), new { id = Image.Id }, Image);
        }

        /// <summary>
        /// Assigns a layout to a image based on ids
        /// </summary>
        /// <param name="imageId"></param>
        /// <param name="layoutId"></param>
        /// <returns>The updated Image</returns>
        [HttpPut("Images/{imageId}/Assign/{layoutId}")]
        [ProducesResponseType(typeof(Image), StatusCodes.Status201Created)]
        [Authorize]
        public async Task<IActionResult> AddImageToLayout(int imageId, int layoutId)
        {
            var image = await _context.Images.FindAsync(imageId);
            var layout = await _context.Layouts.FindAsync(layoutId);
            int userId = Convert.ToInt32(HttpContext.User.FindFirst("UserId")?.Value);
            var user = await _context.Users.FindAsync(userId);

            if (image == null || layout == null || user == null) return NotFound();

            if (user.TenantId != layout.TenantId || user.TenantId != image.TenantId) 
            { 
                return Unauthorized(); 
            }

            image.LayoutId = layoutId;
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetImageById), new { id = image.Id }, image);
        }

        public static byte[] StrToByteArray(string strValue)
        {
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            return encoding.GetBytes(strValue);
        }
    }
}
