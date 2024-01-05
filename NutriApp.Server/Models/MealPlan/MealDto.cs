using NutriApp.Server.DataAccess.Entities.Meals;
using NutriApp.Server.Models.Dish;

namespace NutriApp.Server.Models.MealPlan
{
    public class MealDto
    {
        public Guid Id { get; set; }
        public int GramsOfPortion { get; set; }
        public string MealType { get; set; } = default!;

        public DishDto? Dish { get; set; }
    }
}