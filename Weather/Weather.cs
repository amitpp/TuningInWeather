using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TuningInWeather.Models;

namespace TuningInWeather.Weather
{
    public class Weather : IWeather
    {
        const string apikey = "b77e07f479efe92156376a8b07640ced";
        public async Task<WeatherModel> ReturnWeatherBasedOnCoordinatesAsync(string lat, string longd)
        {
            using var client = new HttpClient();
            var url = new Uri($"http://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={longd}&appid={apikey}");

            var response = await client.GetAsync(url);

            string json;
            using (var content = response.Content)
            {
                json = await content.ReadAsStringAsync();
            }

            return JsonConvert.DeserializeObject<WeatherModel>(json);
        }

        public async Task<WeatherModel> ReturnWeatherBasedOnLocationAsync(string location)
        {
            using var client = new HttpClient();
            var url = new Uri($"http://api.openweathermap.org/data/2.5/weather?q={location}&appid={apikey}");

            var response = await client.GetAsync(url);

            string json;
            using (var content = response.Content)
            {
                json = await content.ReadAsStringAsync();
            }

            return JsonConvert.DeserializeObject<WeatherModel>(json);
        }
    }
}
