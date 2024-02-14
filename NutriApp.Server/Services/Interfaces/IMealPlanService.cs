using NutriApp.Server.Models.MealPlan;

namespace NutriApp.Server.Services.Interfaces
{
    public interface IMealPlanService
    {
        MealPlanDto GetMealPlan(DateTime date);
        void AddToMealPlan(Guid mealPlanId, Guid dishId, uint gramsOfPortion, string mealType);
        void UpdateMealPlan(Guid mealPlanId, UpdateMealPlanRequest updateMealPlanRequest);
        void RemoveMeal(Guid mealPlanId, string mealType);
    }
}