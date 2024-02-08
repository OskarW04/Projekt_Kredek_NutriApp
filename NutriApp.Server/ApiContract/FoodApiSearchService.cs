using Newtonsoft.Json;
using NutriApp.Server.ApiContract.Models;
using NutriApp.Server.Exceptions;
using RestSharp.Authenticators.OAuth2;
using RestSharp;

namespace NutriApp.Server.ApiContract
{
    public class FoodApiSearchService
    {
        public static readonly string BaseSearchUrl = "https://platform.fatsecret.com/rest/server.api";
        private readonly OAuthTokenManager _oAuthTokenManager;

        public FoodApiSearchService(OAuthTokenManager oAuthTokenManager)
        {
            _oAuthTokenManager = oAuthTokenManager;
        }

        public async Task<Foods> FetchFoodSearch(string searchExpression, int pageNumber, int pageSize)
        {
            var token = _oAuthTokenManager.OAuthToken;
            if (string.IsNullOrEmpty(token) ||
                DateTimeOffset.Now.ToUnixTimeMilliseconds() >= _oAuthTokenManager.OAuthTokenExpiration)
            {
                token = await _oAuthTokenManager.FetchAuthenticationToken();
            }

            if (string.IsNullOrEmpty(token))
            {
                throw new FoodDatabaseApiErrorException("Could not fetch external API OAuth");
            }

            var options = new RestClientOptions(BaseSearchUrl)
            {
                Authenticator = new OAuth2UriQueryParameterAuthenticator(token)
            };

            var client = new RestClient(options);
            var request = new RestRequest();
            request.AddParameter("method", "foods.search");
            request.AddParameter("search_expression", searchExpression);
            request.AddParameter("format", "json");
            request.AddParameter("page_number", pageNumber);
            request.AddParameter("max_results", pageSize);
            request.AddParameter("oauth_token", token);

            request.AddHeader("Authorization", "Bearer " + token);

            var response = await client.PostAsync(request);

            if (response.Content != null && (!response.IsSuccessful || response.Content.Contains("error")))
            {
                throw new FoodDatabaseApiErrorException($"Could not fetch foods: {response.Content}");
            }

            var deserializedContent =
                JsonConvert.DeserializeObject<FoodApiResponseRoot>(response.Content ??
                                                                   throw new FoodDatabaseApiErrorException(
                                                                       "Could not fetch foods"));
            if (deserializedContent is null)
            {
                return new Foods()
                {
                    Food = []
                };
            }

            return deserializedContent.Foods;
        }

        public async Task<FoodById> FetchFoodByApiId(string apiId)
        {
            var token = _oAuthTokenManager.OAuthToken;
            if (string.IsNullOrEmpty(token) ||
                DateTimeOffset.Now.ToUnixTimeMilliseconds() >= _oAuthTokenManager.OAuthTokenExpiration)
            {
                token = await _oAuthTokenManager.FetchAuthenticationToken();
            }

            if (token is null)
            {
                throw new FoodDatabaseApiErrorException("Could not fetch external API OAuth");
            }

            var options = new RestClientOptions(BaseSearchUrl)
            {
                Authenticator = new OAuth2UriQueryParameterAuthenticator(token)
            };

            var client = new RestClient(options);
            var request = new RestRequest();
            request.AddParameter("method", "food.get.v3");
            request.AddParameter("food_id", apiId);
            request.AddParameter("format", "json");

            request.AddHeader("Authorization", "Bearer " + token);

            var response = await client.PostAsync(request);

            if (response.Content != null && (!response.IsSuccessful || response.Content.Contains("error")))
            {
                throw new FoodDatabaseApiErrorException("Could not fetch food By Id");
            }

            var deserializedContent =
                JsonConvert.DeserializeObject<FoodByIdResultRoot>(response.Content ??
                                                                  throw new FoodDatabaseApiErrorException(
                                                                      "Could not fetch food By Id"));
            if (deserializedContent?.Food is null)
            {
                return new FoodById()
                {
                    Servings = new Servings()
                    {
                        Serving = []
                    }
                };
            }

            return deserializedContent.Food;
        }
    }
}