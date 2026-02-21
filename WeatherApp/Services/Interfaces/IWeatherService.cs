using WeatherApp.Models;

namespace WeatherApp.Services.Interfaces
{
    public interface IWeatherService
    {
        Task<OpenWeatherResponse> GetWeatherAsync(string city);
    }
}
