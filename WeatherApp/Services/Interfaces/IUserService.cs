using WeatherApp.Entities;

namespace WeatherApp.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> RegisterAsync(string username, string password);
        Task<User?> ValidateUserAsync(string username, string password);
    }
}
