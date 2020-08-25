using System.Threading.Tasks;
using TuningInWeather.Models;
using TuningInWeather.Spotify;


namespace TuningInWeather.Services
{
    public class SpotifyServices: ISpotifyServices
    {
        private static ISpotifyRecommendations _getReco;
        public SpotifyServices(ISpotifyRecommendations getReco)
        {
            _getReco = getReco;
        }
        public async Task<SpotifyModel> SpotifyRecommendation(string countryCode, string genre, string authCode)
        {
            return await _getReco.ReturnSpotifyRecommendation(countryCode, genre, authCode);
        }
        public async Task<AccessToken> GetToken()
        {
            return await _getReco.GetToken();
        }
    }
}
