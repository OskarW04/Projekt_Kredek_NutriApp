using NutriApp.Server.DataAccess.Entities.Meals;
using NutriApp.Server.Models.MealPlan;

namespace NutriApp.Server.Services.Interfaces
{
    public interface IMealPlanService
    {
        MealPlanDto? GetMealPlan(DateTime date);
        void AddToMealPlan(Guid mealPlanId, Guid dishId, uint gramsOfPortion, MealType mealType);
        void UpdateMealPlan(Guid mealPlanId, UpdateMealPlanRequest updateMealPlanRequest);
        void RemoveMeal(Guid mealPlanId, MealType mealType);
    }
}