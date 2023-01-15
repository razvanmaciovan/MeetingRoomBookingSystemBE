using Locus.Controllers;
using Locus.Data;
using Locus.Models;
using Microsoft.EntityFrameworkCore;

namespace Locus.Services
{
    public class UserService
    {
        private readonly EntitiesDbContext _context;
        public UserService(EntitiesDbContext context) => _context = context;

        public bool ValidateLoginInfo(string email, string password)
        {
            User user = _context.Users.FirstOrDefault(u => u.Email == email);

            if (user == null)
                return false;
            //throw new Exception("Invalid user");

            //throw new Exception("User doesn't exist!");

            return PasswordController.Verify(password, user.Password);
            // throw new Exception("Invalid input!");
        }

        public User GetUserByEmail(string email)
        {
            User user = _context.Users.FirstOrDefault(u => u.Email == email);
            return user;
        }
    }
}
