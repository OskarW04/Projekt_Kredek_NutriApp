namespace NutriApp.Server.Models.Product
{
    public class ProductRequest
    {
        public string Name { get; set; } = default!;
        public string? Brand { get; set; }
        public int Calories { get; set; }
        public int Proteins { get; set; }
        public int Carbohydrates { get; set; }
        public int Fats { get; set; }
        public string? Ingredients { get; set; }
        public int GramsInPortion { get; set; } = 100;
    }
}