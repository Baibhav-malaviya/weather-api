namespace WeatherApp.Entities;

public class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;
    public ICollection<WeatherHistory> WeatherHistories { get; set; }
        = new List<WeatherHistory>();
}