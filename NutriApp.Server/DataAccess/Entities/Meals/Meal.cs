using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using NutriApp.Server.DataAccess.Entities.Dishes;
using NutriApp.Server.DataAccess.Entities.MealPlan;

namespace NutriApp.Server.DataAccess.Entities.Meals
{
    // Posilek sklada sie z dania i ilosci gramowej porcji

    public class Meal
    {
        [Key] public Guid Id { get; set; }
        [ForeignKey("DailyMealPlan")] public Guid DailyMealPlanId { get; set; }
        [ForeignKey("Dish")] public Guid DishId { get; set; }
        public MealType MealType { get; set; }
        public int GramsOfPortion { get; set; }

        public virtual Dish? Dish { get; set; }
        public virtual DailyMealPlan? DailyMealPlan { get; set; }
    }
}