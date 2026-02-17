using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeatherApp.Services.Interfaces;

namespace WeatherApiJwt.Controllers;

[ApiController]
[Route("api/[controller]")]
//[Authorize]
public class WeatherController : ControllerBase
{
    private readonly IWeatherService _weatherService;

    public WeatherController(IWeatherService weatherService)
    {
        _weatherService = weatherService;
    }

    [HttpGet("{city}")]
    public async Task<IActionResult> GetWeather(string city)
    {
        var result = await _weatherService.GetWeatherAsync(city);
        return Ok(result);
    }
}
