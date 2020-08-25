using System;
using System.Threading.Tasks;
using TuningInWeather.Models;

namespace TuningInWeather.Weather
{
    public interface IWeather
    {
        Task<WeatherModel> ReturnWeatherBasedOnLocationAsync(string location);
        Task<WeatherModel> ReturnWeatherBasedOnCoordinatesAsync(string lat, string longd);
    }
}
