namespace WeatherApp.Services.Interfaces
{
    public interface IWeatherService
    {
        Task<string> GetWeatherAsync(string city);
    }
}
