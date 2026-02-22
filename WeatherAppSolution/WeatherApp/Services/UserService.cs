using System.Security.Cryptography;
using System.Text;
using WeatherApp.Models;
using WeatherApp.Services.Interfaces;

namespace WeatherApp.Services
{
    public class UserService : IUserService
    {
        private static List<User> _users = new();
        public Task<bool> RegisterAsync(string username, string password)
        {
            if (_users.Any(u => u.Username == username))
                return Task.FromResult(false);

            var user = new User
            {
                Username = username,
                PasswordHash = HashPassword(password)
            };

            _users.Add(user);

            return Task.FromResult(true);
        }

        public Task<User?> ValidateUserAsync(string username, string password)
        {
            var user = _users.FirstOrDefault(u => u.Username == username);

            if(user == null)
            {
                Console.WriteLine("User not registered.");
                return Task.FromResult<User?>(null);
            }

            var hashPassword = HashPassword(password);

            if (hashPassword != user.PasswordHash)
            {
                Console.WriteLine("Incorrect password");
                return Task.FromResult<User?>(null);
            }

            return Task.FromResult(user);
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
