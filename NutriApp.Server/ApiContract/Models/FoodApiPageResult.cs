namespace NutriApp.Server.ApiContract.Models
{
    public class Food
    {
        [Newtonsoft.Json.JsonProperty("food_description")]
        public string FoodDescription { get; set; }

        [Newtonsoft.Json.JsonProperty("food_id")]
        public string FoodId { get; set; }

        [Newtonsoft.Json.JsonProperty("food_name")]
        public string FoodName { get; set; }

        [Newtonsoft.Json.JsonProperty("food_type")]
        public string FoodType { get; set; }

        [Newtonsoft.Json.JsonProperty("food_url")]
        public string FoodUrl { get; set; }

        [Newtonsoft.Json.JsonProperty("brand_name")]
        public string? BrandName { get; set; }
    }

    public class Foods
    {
        [Newtonsoft.Json.JsonProperty("food")] public List<Food> Food { get; set; }

        [Newtonsoft.Json.JsonProperty("max_results")]
        public string MaxResults { get; set; }

        [Newtonsoft.Json.JsonProperty("page_number")]
        public string PageNumber { get; set; }

        [Newtonsoft.Json.JsonProperty("total_results")]
        public string TotalResults { get; set; }
    }

    public class FoodApiResponseRoot
    {
        [Newtonsoft.Json.JsonProperty("foods")]
        public Foods Foods { get; set; }
    }
}