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

    public async Task<string> GetWeatherAsync(string city)
    {
        var baseUrl = _configuration["WeatherApi:BaseUrl"];
        var apiKey = _configuration["WeatherApi:ApiKey"];
        var finalUrl = $"{baseUrl}weather?q={city}&appid={apiKey}&units=metric";

        var response = await _httpClient.GetAsync(finalUrl);

        Console.WriteLine("RESPONSE: " + response);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }
}
