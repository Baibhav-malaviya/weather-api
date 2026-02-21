namespace WeatherApp.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(string username, int id);
    }
}
