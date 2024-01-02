namespace NutriApp.Server.ApiContract.Models
{
    public class FoodByIdResult
    {
        public class FoodById
        {
            public string food_id { get; set; }
            public string food_name { get; set; }
            public string? food_type { get; set; }
            public string? brand_name { get; set; }
            public Servings servings { get; set; }
        }

        public class FoodByIdResultRoot
        {
            public FoodById food { get; set; }
        }

        public class Serving
        {
            public string calories { get; set; }
            public string fat { get; set; }
            public string protein { get; set; }
            public string carbohydrate { get; set; }

            public string? number_of_units { get; set; }
            public string? serving_description { get; set; }
            public string? measurement_description { get; set; }
            public string? metric_serving_amount { get; set; }
            public string? metric_serving_unit { get; set; }
        }

        public class Servings
        {
            public List<Serving> serving { get; set; }
        }
    }
}