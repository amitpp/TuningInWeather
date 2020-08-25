using System.Threading.Tasks;
using TuningInWeather.Models;

namespace TuningInWeather.Services
{
    public interface IWeatherServices
    {

        Task<WeatherModel> GetWeatherBasedOnLocation(string location);
        Task<WeatherModel> GetWeatherBasedOnCoordinates(string lat, string longd);

    }
}
