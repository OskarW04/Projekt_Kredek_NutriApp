using System.Security.Cryptography;
using Newtonsoft.Json;
using System.Text;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Authenticators.OAuth;
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


            var client2 = new HttpClient();
            client2.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var values2 = new Dictionary<string, string>
            {
                { "oauth_signature_method", "HMAC-SHA1" },
                { "method", "foods.search" },
                { "search_expression", "corn" },
                { "format", "json" },
                { "page_number", "0" },
                { "max_results", "10" },
                { "oauth_token", token },
            };

            var content2 = new FormUrlEncodedContent(values2);
            var response2 = await client.PostAsync("https://platform.fatsecret.com/rest/server.api", content2);
            var responseString2 = await response2.Content.ReadAsStringAsync();
            Console.WriteLine(responseString2);

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

        public async void test2()
        {
            // var options = new RestClientOptions
            // {
            //     Authenticator = new OAuth1Authenticator()
            //     {
            //         SignatureMethod = OAuthSignatureMethod.HmacSha1,
            //         ConsumerKey = "2f90ded3168d4c3fa7b4c88dc6138905",
            //         ConsumerSecret = "317ed115a9e346d7948b45fd66b42716",
            //         Verifier = "1.0"
            //     },
            //     BaseUrl = new Uri("https://platform.fatsecret.com/rest/server.api")
            // };
            // var client = new RestClient(options);


            // var signatureBaseString = Escape(httpWebRequest.Method.ToUpper()) + "&";
            // signatureBaseString += EscapeUriDataStringRfc3986(url.ToLower()) + "&";
            // signatureBaseString += EscapeUriDataStringRfc3986(
            //     "oauth_consumer_key=" + EscapeUriDataStringRfc3986(consumerKey) + "&" +
            //     "oauth_nonce=" + EscapeUriDataStringRfc3986(nonce) + "&" +
            //     "oauth_signature_method=" + EscapeUriDataStringRfc3986("HMAC-SHA1") + "&" +
            //     "oauth_timestamp=" + EscapeUriDataStringRfc3986(timeStamp) + "&" +
            //     "oauth_token=" + EscapeUriDataStringRfc3986(tokenValue) + "&" +
            //     "oauth_version=" + EscapeUriDataStringRfc3986("1.0"));
            // Console.WriteLine(@"signatureBaseString: " + signatureBaseString);
            //
            // var key = EscapeUriDataStringRfc3986(consumerSecret) + "&" + EscapeUriDataStringRfc3986(tokenSecret);
            // Console.WriteLine(@"key: " + key);
            // var signatureEncoding = new ASCIIEncoding();
            // var keyBytes = signatureEncoding.GetBytes(key);
            // var signatureBaseBytes = signatureEncoding.GetBytes(signatureBaseString);
            // string signatureString;
            // using (var hmacsha1 = new HMACSHA1(keyBytes))
            // {
            //     var hashBytes = hmacsha1.ComputeHash(signatureBaseBytes);
            //     signatureString = Convert.ToBase64String(hashBytes);
            // }
            // signatureString = EscapeUriDataStringRfc3986(signatureString);
            // Console.WriteLine(@"signatureString: " + signatureString);

            var options = new RestClientOptions("https://platform.fatsecret.com/rest/server.api")
            {
                Authenticator = OAuth1Authenticator.ForRequestToken(
                    "2f90ded3168d4c3fa7b4c88dc6138905",
                    "317ed115a9e346d7948b45fd66b42716")
            };
            var client = new RestClient(options);

            var timeStamp = ((int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds).ToString();
            var nonce = Convert.ToBase64String(Encoding.UTF8.GetBytes(timeStamp));


            //new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds()

            var request = new RestRequest();
            request.AddParameter("oauth_consumer_key", "2f90ded3168d4c3fa7b4c88dc6138905");
            request.AddParameter("oauth_signature_method", "HMAC-SHA1");
            request.AddParameter("oauth_timestamp", timeStamp);
            request.AddParameter("oauth_version", "1.0");

            request.AddParameter("oauth_nonce", nonce);
            // request.AddParameter("oauth_signature", "9bNakGgnvvJIg3W7eVwqhK8Mckc%3D");

            request.AddParameter("method", "foods.search");
            request.AddParameter("format", "json");
            request.AddParameter("page_number", "0");
            request.AddParameter("max_results", "10");
            request.AddParameter("search_expression", "toast");

            var response = await client.PostAsync(request);
            var content = response.Content;
            Console.WriteLine(content);
        }

        string Escape(string s)
        {
            // https://stackoverflow.com/questions/846487/how-to-get-uri-escapedatastring-to-comply-with-rfc-3986
            var charsToEscape = new[] { "!", "*", "'", "(", ")" };
            var escaped = new StringBuilder(Uri.EscapeDataString(s));
            foreach (var t in charsToEscape)
            {
                escaped.Replace(t, Uri.HexEscape(t[0]));
            }

            return escaped.ToString();
        }
    }
}