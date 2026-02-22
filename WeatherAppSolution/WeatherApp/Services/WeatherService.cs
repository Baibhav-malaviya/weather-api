using System.Text.Json;
using WeatherApp.Models;
using WeatherApp.Services.Interfaces;

namespace WeatherApiJwt.Services;

public class WeatherService : IWeatherService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public WeatherService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<OpenWeatherResponse> GetWeatherAsync(string city)
    {
        var baseUrl = _configuration["WeatherApi:BaseUrl"];
        var apiKey = _configuration["WeatherApi:ApiKey"];

        var finalUrl = $"{baseUrl}weather?q={city}&appid={apiKey}&units=metric";

        var response = await _httpClient.GetAsync(finalUrl);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Weather API call failed");
        }

        var json = await response.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<OpenWeatherResponse>(
            json,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

        if (result == null)
        {
            throw new Exception("Failed to deserialize weather response");
        }

        return result;
    }
}