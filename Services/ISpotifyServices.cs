using System;
using System.Threading.Tasks;
using TuningInWeather.Models;

namespace TuningInWeather.Services
{
    public interface ISpotifyServices
    {
        Task<SpotifyModel> SpotifyRecommendation(string countryCode, string genre, string authCode);
        Task<AccessToken> GetToken();
    }
}
