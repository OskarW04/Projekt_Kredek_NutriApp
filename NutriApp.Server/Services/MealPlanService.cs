using NutriApp.Server.DataAccess.Entities.Meals;
using NutriApp.Server.Exceptions;
using NutriApp.Server.Models.MealPlan;
using NutriApp.Server.Repositories.Interfaces;
using NutriApp.Server.Services.Interfaces;

namespace NutriApp.Server.Services
{
    public class MealPlanService : IMealPlanService
    {
        private static readonly Dictionary<string, MealType> MealTypes = new()
        {
            { "breakfast", MealType.Breakfast },
            { "secondbreakfast", MealType.SecondBreakfast },
            { "lunch", MealType.Lunch },
            { "dinner", MealType.Dinner },
            { "snack", MealType.Snack },
            { "supper", MealType.Supper }
        };

        private readonly IMealPlanRepository _mealPlanRepository;
        private readonly IUserContextService _userContextService;

        public MealPlanService(IMealPlanRepository mealPlanRepository, IUserContextService userContextService)
        {
            _mealPlanRepository = mealPlanRepository;
            _userContextService = userContextService;
        }

        public MealPlanDto GetMealPlan(DateTime date)
        {
            var userId = VerifyUserClaims();
            return _mealPlanRepository.GetByDate(date, userId);
        }

        public void AddToMealPlan(Guid mealPlanId, Guid dishId, uint gramsOfPortion, string mealType)
        {
            var userId = VerifyUserClaims();

            var check = MealTypes.TryGetValue(mealType.ToLower(), out var type);
            if (!check)
            {
                throw new IncorrectInputTypeException("Meal type invalid");
            }

            _mealPlanRepository.AddToMealPlan(mealPlanId, dishId, gramsOfPortion, type, userId);
        }

        public void UpdateMealPlan(Guid mealPlanId, UpdateMealPlanRequest updateMealPlanRequest)
        {
            var userId = VerifyUserClaims();
            _mealPlanRepository.UpdateMealPlan(mealPlanId, updateMealPlanRequest, userId);
        }

        public void RemoveMeal(Guid mealPlanId, string mealType)
        {
            var userId = VerifyUserClaims();

            var check = MealTypes.TryGetValue(mealType.ToLower(), out var type);
            if (!check)
            {
                throw new IncorrectInputTypeException("Meal type invalid");
            }

            _mealPlanRepository.RemoveMeal(mealPlanId, type, userId);
        }

        private string VerifyUserClaims()
        {
            var userId = _userContextService.UserId;
            if (userId is null)
            {
                throw new ForbidException("User claims invalid");
            }

            return userId;
        }
    }
}