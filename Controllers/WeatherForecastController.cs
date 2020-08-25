using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TuningInWeather.Models;
using TuningInWeather.Services;

namespace TuningInWeather.Controllers
{
    [ApiController]
    [Route("{location}")]
    [Route("{lat}/{longd}")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherServices _openWeatherServices;
        private readonly ISpotifyServices _spotifyServices;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherServices openWeatherServices, ISpotifyServices spotifyServices)
        {
            _logger = logger;
            _openWeatherServices = openWeatherServices;
            _spotifyServices = spotifyServices;

        }

        [HttpGet]
        public async Task<WeatherForecast> Get(string location=null, string lat=null, string longd=null)
        {
            var weatherData = (WeatherModel)null;
            var spotifyData = (SpotifyModel)null;
            List<string> tracks = new List<string>();
            var authCode = await _spotifyServices.GetToken();

            if (location != null)
            {
                weatherData = await _openWeatherServices.GetWeatherBasedOnLocation(location);
            }
            else
            {
                weatherData = await _openWeatherServices.GetWeatherBasedOnCoordinates(lat, longd);
            }
            var temp = (int)((int)weatherData.Main.Temp - 273.15);
            if (temp < 10)
            {
                spotifyData = await _spotifyServices.SpotifyRecommendation(weatherData.Sys.Country, "Classical", authCode.access_token);
            }
            else if ((temp >= 10) && (temp <= 14))
            {
                spotifyData = await _spotifyServices.SpotifyRecommendation(weatherData.Sys.Country, "rock", authCode.access_token);
            }
            else if ((temp >= 15) && (temp <= 30))
            {
                spotifyData = await _spotifyServices.SpotifyRecommendation(weatherData.Sys.Country, "pop", authCode.access_token);
            }
            else
            {
                spotifyData = await _spotifyServices.SpotifyRecommendation(weatherData.Sys.Country, "party", authCode.access_token);
            }
            foreach (var recommendation in spotifyData.tracks)
            {
                tracks.Add(recommendation.name);
            }
            return new WeatherForecast
            {
                Date = DateTime.Now,
                Temp = temp,
                Tracks = tracks,
            };
        }

    }
}
