using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TuningInWeather.Models;
using System.Collections.Generic;
using System.Text;

namespace TuningInWeather.Spotify
{
    public class SpotifyRecommendations: ISpotifyRecommendations
    {

        public async Task<AccessToken> GetToken()
        {
            {
            string clientId = "d9c11585a85049f59e8e1949825adbb9";
            string clientSecret = "61c6df7adea844799c7f996e2e67cca4";
            string credentials = String.Format("{0}:{1}",clientId,clientSecret);

            using(var client = new HttpClient())
            {
                //Define Headers
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials)));

                //Prepare Request Body
                List<KeyValuePair<string,string>> requestData = new List<KeyValuePair<string,string>>();
                requestData.Add(new KeyValuePair<string,string>("grant_type","client_credentials"));

                FormUrlEncodedContent requestBody = new FormUrlEncodedContent(requestData);

                //Request Token
                var request = await client.PostAsync("https://accounts.spotify.com/api/token",requestBody);
                var response = await request.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<AccessToken>(response);
            }
        }
        }
        // const string authCode = "BQDWGXx0kGTa_jL305L-OBeF_9gxCXAtcKb-WgDAQv35vwS48QC5TlZ7gpUrDCLaKMRqtoqI1fFFyoXwqMc";
        public async Task<SpotifyModel> ReturnSpotifyRecommendation(string countryCode, string genre, string authCode)
        {
            using var client = new HttpClient();
            var url = new Uri($"https://api.spotify.com/v1/recommendations?market={countryCode}&seed_genres={genre}");
            client.DefaultRequestHeaders.Authorization
                         = new AuthenticationHeaderValue("Bearer", authCode);
            var response = await client.GetAsync(url);
            string json;
            using (var content = response.Content)
            {
                json = await content.ReadAsStringAsync();
            }
            return JsonConvert.DeserializeObject<SpotifyModel>(json);

        }
    }
}
