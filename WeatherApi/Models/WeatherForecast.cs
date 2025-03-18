namespace WeatherApi.Models
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }
        public string City { get; set; } = string.Empty;
        public double TemperatureC { get; set; }
        public double TemperatureF => 32 + (TemperatureC * 9 / 5);
        public string Summary { get; set; } = string.Empty;
    }
}
