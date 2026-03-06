using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeatherApp.Data;
using WeatherApp.DTOs;
using WeatherApp.Entities;
using WeatherApp.Services.Interfaces;

namespace WeatherApiJwt.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class WeatherController : ControllerBase
{
    private readonly IWeatherService _weatherService;
    private readonly AppDbContext _context;
    private readonly ILogger<WeatherController> _logger;

    public WeatherController(
        IWeatherService weatherService,
        AppDbContext context,
        ILogger<WeatherController> logger)
    {
        _weatherService = weatherService;
        _context = context;
        _logger = logger;
    }

    [HttpGet("{city}")]
    public async Task<IActionResult> GetWeather(string city)
    {

        //todo: Extract userId from JWT
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userIdClaim == null)
        {
            _logger.LogInformation("User is unauthorized");
            return Unauthorized();
        }

        var userId = int.Parse(userIdClaim);

        var weather = await _weatherService.GetWeatherAsync(city);

        // Save history
        var history = new WeatherHistory
        {
            UserId = userId,
            City = weather.Name,
            Temp = weather.Main.Temp,
            TempMin = weather.Main.Temp_Min,
            TempMax = weather.Main.Temp_Max,
            Pressure = weather.Main.Pressure,
            FeelsLike = weather.Main.Feels_Like,
            Humidity = weather.Main.Humidity,
            GrndLevel = weather.Main.Grnd_Level ?? 0,
            SeaLevel = weather.Main.Sea_Level ?? 0
        };

        _context.WeatherHistories.Add(history);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Weather fetched successfully {@weather}", weather);

        return Ok(weather);
    }

    [HttpGet("history")]
    public async Task<IActionResult> GetHistory(
    [FromQuery] int page = 1,
    [FromQuery] int pageSize = 10)
    {
        if (page <= 0) page = 1;
        if (pageSize <= 0) pageSize = 10;

        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userIdClaim == null)
            return Unauthorized();

        var userId = int.Parse(userIdClaim);

        var query = _context.WeatherHistories
            .Where(w => w.UserId == userId)
            .OrderByDescending(w => w.RequestedAt);

        var totalCount = await query.CountAsync();

        var data = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(w => new WeatherHistoryDto
            {
                City = w.City,
                Temp = w.Temp,
                FeelsLike = w.FeelsLike,
                TempMin = w.TempMin,
                TempMax = w.TempMax,
                Pressure = w.Pressure,
                Humidity = w.Humidity,
                RequestedAt = w.RequestedAt
            })
            .ToListAsync();

        _logger.LogInformation("History fetched successfully {@data}", data);

        return Ok(new
        {
            page,
            pageSize,
            totalCount,
            totalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
            data
        });
    }
}