using Newtonsoft.Json;
using System.Text;
using RestSharp;
using RestSharp.Authenticators.OAuth2;

namespace NutriApp.Server.ApiContract
{
    public class Test
    {
        private static readonly string _clientId = "2f90ded3168d4c3fa7b4c88dc6138905";
        private static readonly string _clientSecret = "af0afa51986c40b5a069330ddaf8f0a9";

        public async void test()
        {
            var client = new HttpClient();
            var byteArray = Encoding.ASCII.GetBytes(_clientId + ":" + _clientSecret);
            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            var values = new Dictionary<string, string>
            {
                { "scope", "basic" },
                { "grant_type", "client_credentials" }
            };
            var content = new FormUrlEncodedContent(values);
            var response = await client.PostAsync("https://oauth.fatsecret.com/connect/token", content);

            var responseString = await response.Content.ReadAsStringAsync();
            var token = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseString)?["access_token"];
            Console.WriteLine(responseString);


            // var client2 = new HttpClient();
            // client2.DefaultRequestHeaders.Authorization =
            //     new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            // var values2 = new Dictionary<string, string>
            // {
            //     { "oauth_signature_method", "HMAC-SHA1" },
            //     { "method", "foods.search" },
            //     { "search_expression", "corn" },
            //     { "format", "json" },
            //     { "page_number", "0" },
            //     { "max_results", "10" },
            //     { "oauth_token", token },
            // };
            //
            // var content2 = new FormUrlEncodedContent(values2);
            // var response2 = await client.PostAsync("https://platform.fatsecret.com/rest/server.api", content2);
            // var responseString2 = await response2.Content.ReadAsStringAsync();
            // Console.WriteLine(responseString2);

            ///////////////////////////////

            var options = new RestClientOptions("https://platform.fatsecret.com/rest/server.api")
            {
                Authenticator = new OAuth2UriQueryParameterAuthenticator(token)
            };
            var client33 = new RestClient(options);
            var request = new RestRequest();
            request.AddParameter("method", "foods.search");
            request.AddParameter("search_expression", "corn");
            request.AddParameter("format", "json");
            request.AddParameter("page_number", "0");
            request.AddParameter("max_results", "10");
            request.AddParameter("oauth_token", token);

            request.AddHeader("Authorization", "Bearer " + token);

            var response33 = await client33.PostAsync(request);
            var content33 = response33.Content;
            Console.WriteLine(content33);
        }
    }
}