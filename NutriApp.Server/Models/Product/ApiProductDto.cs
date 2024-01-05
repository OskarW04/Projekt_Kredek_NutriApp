namespace NutriApp.Server.Models.Product
{
    public class ApiProductDto
    {
        public string ApiUrl { get; set; } = default!;
        public string ApiId { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string? Brand { get; set; } = default!;
        public string? Description { get; set; } = default!;
        public string? Portion { get; set; } = default!;

        public int Calories { get; set; }
        public int Proteins { get; set; }
        public int Carbohydrates { get; set; }
        public int Fats { get; set; }
        public int GramsInPortion { get; set; } = 100;
        public int? Amount { get; set; }
    }
}