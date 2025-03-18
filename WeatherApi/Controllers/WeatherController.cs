using System.Collections.Concurrent;
using System.Diagnostics.Eventing.Reader;
using Microsoft.AspNetCore.Mvc;
using WeatherApi.Models;
using WeatherApi.Services;

namespace WeatherApi.Controllers
{
    [ApiController]
    [Route("weather")]
    public class WeatherController : ControllerBase
    {
        private readonly WeatherService _weatherService;


        public WeatherController()
        {
            _weatherService = new WeatherService();
        }

        [HttpGet("today")]
        public ActionResult<WeatherForecast> GetTodayWeather([FromQuery] string city)
        {
            // define rate limmit parameters for today api call 
            var key = HttpContext.Connection.RemoteIpAddress?.ToString() + "-day" ?? "unknown";
            RateLimit rateLimit_today = new RateLimit(5, TimeSpan.FromMinutes(1), key);

            if (string.IsNullOrWhiteSpace(city))
                return BadRequest("City parameter is required.");
            
            if (!rateLimit_today.IsRequestValid())
                return StatusCode(429, "Too many requests. Please try again later.");

            var weather = _weatherService.GetTodayWeather(city);
            return Ok(weather);
        }

        [HttpGet("week")]
        public ActionResult<List<WeatherForecast>> GetWeeklyWeather([FromQuery] string city)
        {
            // d
            var key = HttpContext.Connection.RemoteIpAddress?.ToString() + "-week" ?? "unknown";
            RateLimit rateLimit_week = new RateLimit(10, TimeSpan.FromMinutes(1), key);
            if (string.IsNullOrWhiteSpace(city))
                return BadRequest("City parameter is required.");

            if (!rateLimit_week.IsRequestValid())
                return StatusCode(429, "Too many requests. Please try again later.");

            var weather = _weatherService.GetWeeklyWeather(city);
            return Ok(weather);
        }
    }
    
}