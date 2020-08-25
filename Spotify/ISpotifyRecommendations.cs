using System;
using System.Threading.Tasks;
using TuningInWeather.Models;

namespace TuningInWeather.Spotify
{
    public interface ISpotifyRecommendations
    {
        Task<SpotifyModel> ReturnSpotifyRecommendation(string countryCode, string genre, string authCode);
        Task<AccessToken> GetToken();
    }
}
