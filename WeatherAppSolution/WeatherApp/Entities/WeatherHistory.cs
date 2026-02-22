namespace WeatherApp.Entities
{
    public class WeatherHistory
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public string City { get; set; } = null!;

        public double Temp { get; set; }
        public double TempMin { get; set; }
        public double TempMax { get; set; }
        public double Pressure { get; set; }
        public double FeelsLike { get; set; }
        public double GrndLevel { get; set; }
        public double SeaLevel { get; set; }
        public double Humidity { get; set; }

        public DateTime RequestedAt { get; set; } = DateTime.UtcNow;
    }
}
