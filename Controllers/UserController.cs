using Locus.Data;
using Locus.Models;
using Locus.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;

namespace Locus.Controllers
{
    [Route("/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly EntitiesDbContext _context;
        public UserController(EntitiesDbContext context) => _context = context;

        [HttpGet("Users")]
        [Authorize("admin:True")]
        public async Task<IEnumerable<User>> GetUsers() => await _context.Users.ToListAsync();

        [HttpGet("Users/{id}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _context.Users.FindAsync(id);
            int userJWTTenantId = Convert.ToInt32(HttpContext.User.FindFirst("TenantId")?.Value);

            if (user == null || userJWTTenantId == 0)
            {
                return NotFound();
            }
            if (user.TenantId != userJWTTenantId)
            {
                return Unauthorized();
            }

            return Ok(user);
        }
        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <param name="user">Object of type User</param>
        /// <returns>The user created</returns>
        [HttpPost("Users")]
        [ProducesResponseType(typeof(User), StatusCodes.Status201Created)]
        [Authorize("admin:True")]
        public async Task<IActionResult> Create(User user)
        {
            user.Password = PasswordController.Hash(user.Password);
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
        [Authorize("admin:True")]
        public async Task<IActionResult> AddUserToTenant(int userId, int tenantId)
        {
            var user = await _context.Users.FindAsync(userId);
            var tenant = await _context.Tenants.FindAsync(tenantId);
            int userJWTTenantId = Convert.ToInt32(HttpContext.User.FindFirst("TenantId")?.Value);

            if (user == null || tenant == null || userJWTTenantId == 0)
            {
                return NotFound();
            }
            if (user.TenantId != userJWTTenantId || userJWTTenantId != tenantId)
            {
                return Unauthorized();
            }

            user.TenantId = tenantId;
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            if (request == null)
                return BadRequest("Invalid client request");
            User user = _context.Users.FirstOrDefault(u => u.Email == request.Email);

            if (user == null)
                return NoContent();
            //throw new Exception("Invalid user");

            //throw new Exception("User doesn't exist!");

            if (PasswordController.Verify(request.Password, user.Password) == false)
                return Unauthorized();

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
            var sigingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokenOptions = new JwtSecurityToken(
                issuer: "https://localhost:7122",
                audience: "https://localhost:7122",
                claims: new List<Claim>()
                {
                    new Claim("UserId", user.Id.ToString()),
                    new Claim("Email", user.Email),
                    new Claim("admin", user.IsAdmin.ToString()),
                    new Claim("TenantId", user.TenantId.ToString())
                },
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: sigingCredentials);
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return Ok(new { Token = tokenString });
        }

    }
}