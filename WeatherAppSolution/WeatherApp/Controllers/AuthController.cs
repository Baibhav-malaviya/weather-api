using Microsoft.AspNetCore.Mvc;
using WeatherApp.Models;
using WeatherApp.Services.Interfaces;

namespace WeatherApiJwt.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly IUserService _userService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(
        ITokenService tokenService,
        IUserService userService,
        ILogger<AuthController> logger)
    {
        _tokenService = tokenService;
        _userService = userService;
        _logger = logger;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(Register request)
    {
        _logger.LogInformation("Welcome to register {@request}", request);
        var success = await _userService
            .RegisterAsync(request.Username, request.Password);

        if (!success)
            return BadRequest("User already exists.");

        return Ok("User registered successfully.");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(Login request)
    {
        _logger.LogInformation("Welcome to register {@request}", request);
        var user = await _userService
            .ValidateUserAsync(request.Username, request.Password);

        if (user == null)
            return Unauthorized();

        var token = _tokenService.GenerateToken(user.Username, user.Id);

        return Ok(new { token });
    }
}
