using WeatherApi.Models;

namespace WeatherApi.Services
{
    public class WeatherService
    {
        private static readonly string[] Summaries = new[]
        {
            "Sunny", "Cloudy", "Rainy", "Stormy", "Snowy", "Windy", "Foggy"
        };

        private readonly Random _random = new();

        public WeatherForecast GetTodayWeather(string city)
        {
            return new WeatherForecast
            {
                Date = DateTime.UtcNow,
                City = city,
                TemperatureC = _random.Next(-10, 35),
                Summary = Summaries[_random.Next(Summaries.Length)]
            };
        }

        public List<WeatherForecast> GetWeeklyWeather(string city)
        {
            return [.. Enumerable.Range(0, 7).Select(day => new WeatherForecast
            {
                Date = DateTime.UtcNow.AddDays(day),
                City = city,
                TemperatureC = _random.Next(-10, 35),
                Summary = Summaries[_random.Next(Summaries.Length)]
            })];
        }
    }
}
