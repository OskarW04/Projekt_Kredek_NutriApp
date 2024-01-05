using NutriApp.Server.DataAccess.Entities.Meals;
using NutriApp.Server.Models.MealPlan;

namespace NutriApp.Server.Repositories.Interfaces
{
    public interface IMealPlanRepository
    {
        MealPlanDto? GetByDate(DateTime date, string userId);
        void AddToMealPlan(Guid mealPlanId, Guid dishId, uint gramsOfPortion, MealType mealType, string userId);
    }
}