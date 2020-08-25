using System.Threading.Tasks;
using TuningInWeather.Models;
using TuningInWeather.Weather;

namespace TuningInWeather.Services
{
    public class WeatherServices: IWeatherServices
    {
        private static IWeather _getWeather;
        public WeatherServices(IWeather getWeather)
        {
            _getWeather = getWeather;
        }
        public async Task<WeatherModel> GetWeatherBasedOnLocation(string location)
        {
            return await _getWeather.ReturnWeatherBasedOnLocationAsync(location);
        }
        public async Task<WeatherModel> GetWeatherBasedOnCoordinates(string lat, string longd)
        {
            return await _getWeather.ReturnWeatherBasedOnCoordinatesAsync(lat,longd);
        }
    }
}
