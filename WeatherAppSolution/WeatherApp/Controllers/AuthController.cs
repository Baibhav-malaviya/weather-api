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

    public AuthController(
        ITokenService tokenService,
        IUserService userService)
    {
        _tokenService = tokenService;
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(Register request)
    {
        Console.WriteLine("Welcome to Register");
        var success = await _userService
            .RegisterAsync(request.Username, request.Password);

        if (!success)
            return BadRequest("User already exists.");

        return Ok("User registered successfully.");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(Login request)
    {
        Console.WriteLine("Welcome to login");
        var user = await _userService
            .ValidateUserAsync(request.Username, request.Password);

        if (user == null)
            return Unauthorized();

        var token = _tokenService.GenerateToken(user.Username);

        return Ok(new { token });
    }
}
