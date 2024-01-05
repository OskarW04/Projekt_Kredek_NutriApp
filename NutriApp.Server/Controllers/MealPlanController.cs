using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NutriApp.Server.DataAccess.Entities.Meals;
using NutriApp.Server.Models.MealPlan;
using NutriApp.Server.Services.Interfaces;

namespace NutriApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MealPlanController : ControllerBase
    {
        // TODO: Moze dodac mozliwosc dodawania samych produktow do meals -> nowa tabela MealProducts oraz MealApiProducts

        private readonly IMealPlanService _mealPlanService;

        public MealPlanController(IMealPlanService mealPlanService)
        {
            _mealPlanService = mealPlanService;
        }

        [HttpGet("{date}")]
        public ActionResult<MealPlanDto> Get(
            [FromRoute] DateTime date)
        {
            var mealPlan = _mealPlanService.GetMealPlan(date);
            return Ok(mealPlan);
        }

        [HttpPut("{mealPlanId}")]
        public ActionResult AddMeal(
            [FromRoute] Guid mealPlanId,
            [FromQuery] Guid dishId,
            [FromQuery] uint gramsOfPortion,
            [FromQuery] MealType mealType)
        {
            _mealPlanService.AddToMealPlan(mealPlanId, dishId, gramsOfPortion, mealType);
            return Ok();
        }
    }
}