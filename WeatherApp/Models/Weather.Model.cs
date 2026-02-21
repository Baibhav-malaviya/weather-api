namespace WeatherApp.Models
{
    public class OpenWeatherResponse
    {
        public string Name { get; set; } = null!;
        public WeatherMain Main { get; set; } = null!;
    }

    public class WeatherMain
    {
        public double Temp { get; set; }
        public double Temp_Min { get; set; }
        public double Temp_Max { get; set; }
        public double Pressure { get; set; }
        public double Feels_Like { get; set; }
        public double Humidity { get; set; }
        public double? Grnd_Level { get; set; }
        public double? Sea_Level { get; set; }
    }
}
