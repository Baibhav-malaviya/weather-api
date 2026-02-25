using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

using WeatherApp.Data;
using WeatherApp.Entities;
using WeatherApp.Services.Interfaces;

namespace WeatherApp.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> RegisterAsync(string username, string password)
        {
            // check if user alrady exist
            if (await _context.Users.AnyAsync(u => u.Username == username))
                return false;

            var user = new User
            {
                Username = username,
                PasswordHash = HashPassword(password)
            };

            // add new user to the dbs
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return true ;
        }

        public async Task<User?> ValidateUserAsync(string username, string password)
        {
            // check if user exist
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username);

            if (user == null)
                return null;

            var hashPassword = HashPassword(password);

            // check for password match
            if (user.PasswordHash != hashPassword)
                return null;

            return user;
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
