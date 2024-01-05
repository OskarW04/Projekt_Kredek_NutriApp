using System.Text;
using Newtonsoft.Json;
using NutriApp.Server.ApiContract.Settings;

namespace NutriApp.Server.ApiContract
{
    public class OAuthTokenManager
    {
        public static readonly string BaseAuthUrl = "https://oauth.fatsecret.com/connect/token";
        private readonly AuthenticationKeys _authenticationKeys;

        public string? OAuthToken { get; private set; }
        public long OAuthTokenValidity { get; private set; }
        public long OAuthTokenExpiration { get; private set; }

        public OAuthTokenManager(AuthenticationKeys authenticationKeys)
        {
            _authenticationKeys = authenticationKeys;
        }

        public async Task<string?> FetchAuthenticationToken()
        {
            using var client = new HttpClient();
            var byteArray =
                Encoding.ASCII.GetBytes(_authenticationKeys.ClientId + ":" + _authenticationKeys.ClientSecret);

            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            var values = new Dictionary<string, string>
            {
                { "scope", "basic" },
                { "grant_type", "client_credentials" }
            };
            var content = new FormUrlEncodedContent(values);
            var response = await client.PostAsync(BaseAuthUrl, content);
            var responseString = await response.Content.ReadAsStringAsync();

            OAuthToken = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseString)?["access_token"];

            var isSuccess = long.TryParse(
                JsonConvert.DeserializeObject<Dictionary<string, string>>(responseString)?["expires_in"],
                out var value);
            OAuthTokenValidity = isSuccess ? value : 0;

            OAuthTokenExpiration = DateTimeOffset.Now.ToUnixTimeMilliseconds() + OAuthTokenValidity;

            return OAuthToken;
        }
    }
}