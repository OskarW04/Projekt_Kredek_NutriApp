using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        // Todo: Mozliwosc dodawania wiekszej ilosci dan do jednego posilku

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
            [FromQuery] string mealType)
        {
            _mealPlanService.AddToMealPlan(mealPlanId, dishId, gramsOfPortion, mealType);
            return Ok();
        }

        [HttpPut("update/{mealPlanId}")]
        public ActionResult UpdateMealPlan(
            [FromRoute] Guid mealPlanId,
            [FromBody] UpdateMealPlanRequest updateMealPlanRequest)
        {
            _mealPlanService.UpdateMealPlan(mealPlanId, updateMealPlanRequest);
            return Ok();
        }

        [HttpDelete("{mealPlanId}")]
        public ActionResult RemoveMeal(
            [FromRoute] Guid mealPlanId,
            [FromQuery] string mealType)
        {
            _mealPlanService.RemoveMeal(mealPlanId, mealType);
            return Ok();
        }
    }
}