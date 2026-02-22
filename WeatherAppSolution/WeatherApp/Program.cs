using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WeatherApiJwt.Services;
using WeatherApiJwt.Settings;
using WeatherApp.Data;
using WeatherApp.Services;
using WeatherApp.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Bind settings
builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("JwtSettings"));

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Register services
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IWeatherService, WeatherService>();
builder.Services.AddHttpClient<IWeatherService, WeatherService>();
builder.Services.AddScoped<IUserService, UserService>();


builder.Services.AddControllers();

// JWT Configuration
var jwtSettings = builder.Configuration
    .GetSection("JwtSettings")
    .Get<JwtSettings>();

var key = Encoding.UTF8.GetBytes(jwtSettings.Key);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy
            .WithOrigins("http://localhost:4200")  // Angular dev server
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAny", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddAuthorization();

var app = builder.Build();


app.UseCors("AllowAny");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.Run();
