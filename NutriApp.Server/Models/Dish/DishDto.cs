using NutriApp.Server.Models.Product;

namespace NutriApp.Server.Models.Dish
{
    public class DishDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public int GramsTotal { get; set; }
        public int Calories { get; set; }
        public int Proteins { get; set; }
        public int Carbohydrates { get; set; }
        public int Fats { get; set; }

        public List<ProductDto>? DishProducts { get; set; }
        public List<ApiProductDto>? DishApiProducts { get; set; }
    }
}