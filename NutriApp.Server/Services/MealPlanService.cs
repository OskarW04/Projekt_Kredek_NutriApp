using NutriApp.Server.DataAccess.Entities.Meals;
using NutriApp.Server.Exceptions;
using NutriApp.Server.Models.MealPlan;
using NutriApp.Server.Repositories.Interfaces;
using NutriApp.Server.Services.Interfaces;

namespace NutriApp.Server.Services
{
    public class MealPlanService : IMealPlanService
    {
        private readonly IMealPlanRepository _mealPlanRepository;
        private readonly IUserContextService _userContextService;

        public MealPlanService(IMealPlanRepository mealPlanRepository, IUserContextService userContextService)
        {
            _mealPlanRepository = mealPlanRepository;
            _userContextService = userContextService;
        }

        public MealPlanDto? GetMealPlan(DateTime date)
        {
            var userId = VerifyUserClaims();
            return _mealPlanRepository.GetByDate(date, userId);
        }

        public void AddToMealPlan(Guid mealPlanId, Guid dishId, uint gramsOfPortion, MealType mealType)
        {
            var userId = VerifyUserClaims();
            _mealPlanRepository.AddToMealPlan(mealPlanId, dishId, gramsOfPortion, mealType, userId);
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