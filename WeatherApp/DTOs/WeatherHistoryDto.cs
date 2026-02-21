namespace WeatherApp.DTOs
{
    public class WeatherHistoryDto
    {
        public string City { get; set; } = null!;
        public double Temp { get; set; }
        public double FeelsLike { get; set; }
        public double TempMin { get; set; }
        public double TempMax { get; set; }
        public double Pressure { get; set; }
        public double Humidity { get; set; }
        public DateTime RequestedAt { get; set; }
    }
}
