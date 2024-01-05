namespace NutriApp.Server.ApiContract.Models
{
    public class FoodById
    {
        [Newtonsoft.Json.JsonProperty("food_id")]
        public string FoodId { get; set; }

        [Newtonsoft.Json.JsonProperty("food_name")]
        public string FoodName { get; set; }

        [Newtonsoft.Json.JsonProperty("food_type")]
        public string? FoodType { get; set; }

        [Newtonsoft.Json.JsonProperty("food_url")]
        public string FoodUrl { get; set; }

        [Newtonsoft.Json.JsonProperty("brand_name")]
        public string? BrandName { get; set; }

        [Newtonsoft.Json.JsonProperty("servings")]
        public Servings Servings { get; set; }
    }

    public class FoodByIdResultRoot
    {
        [Newtonsoft.Json.JsonProperty("food")] public FoodById Food { get; set; }
    }

    public class Serving
    {
        [Newtonsoft.Json.JsonProperty("calories")]
        public string Calories { get; set; }

        [Newtonsoft.Json.JsonProperty("fat")] public string Fat { get; set; }

        [Newtonsoft.Json.JsonProperty("protein")]
        public string Protein { get; set; }

        [Newtonsoft.Json.JsonProperty("carbohydrate")]
        public string Carbohydrate { get; set; }

        [Newtonsoft.Json.JsonProperty("number_of_units")]
        public string? NumberOfUnits { get; set; }

        [Newtonsoft.Json.JsonProperty("serving_description")]
        public string? ServingDescription { get; set; }

        [Newtonsoft.Json.JsonProperty("measurement_description")]
        public string? MeasurementDescription { get; set; }

        [Newtonsoft.Json.JsonProperty("metric_serving_amount")]
        public string? MetricServingAmount { get; set; }

        [Newtonsoft.Json.JsonProperty("metric_serving_unit")]
        public string? MetricServingUnit { get; set; }
    }

    public class Servings
    {
        [Newtonsoft.Json.JsonProperty("serving")]
        public List<Serving> Serving { get; set; }
    }
}