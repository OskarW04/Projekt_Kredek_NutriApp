using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NutriApp.Server.Models;
using NutriApp.Server.Models.Dish;
using NutriApp.Server.Services.Interfaces;

namespace NutriApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DishController : ControllerBase
    {
        private readonly IDishService _dishService;

        public DishController(IDishService dishService)
        {
            _dishService = dishService;
        }

        [HttpGet("userDishes")]
        public ActionResult<PageResult<DishDto>> GetUsersDishes(
            [FromBody] PaginationParams paginationParams)
        {
            var dishes = _dishService.GetDishes(paginationParams);
            return Ok(dishes);
        }

        [HttpGet("{id}")]
        public ActionResult<DishDto> GetDish(
            [FromRoute] Guid id)
        {
            var dish = _dishService.GetDish(id);
            return Ok(dish);
        }

        [HttpPost]
        public ActionResult AddDish(
            [FromBody] DishRequest addDishRequest)
        {
            var dishId = _dishService.AddDish(addDishRequest);
            return Created($"/api/Dish/{dishId}", null);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(
            [FromRoute] Guid id)
        {
            _dishService.DeleteDish(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public ActionResult UpdateDish(
            [FromRoute] Guid id,
            [FromBody] DishRequest updateDishRequest)
        {
            _dishService.UpdateDish(id, updateDishRequest);
            return Ok();
        }

        [HttpPut("{dishId}/addProduct")]
        public async Task<ActionResult> AddDishProduct(
            [FromRoute] Guid dishId,
            [FromQuery] string productId,
            [FromQuery] uint grams)
        {
            await _dishService.AddProduct(dishId, productId, grams);
            return Ok();
        }

        [HttpPut("{dishId}/updateProduct")]
        public ActionResult UpdateDishProductPortion(
            [FromRoute] Guid dishId,
            [FromQuery] string productId,
            [FromQuery] uint grams)
        {
            _dishService.UpdatePortion(dishId, productId, grams);
            return Ok();
        }

        [HttpDelete("{dishId}/removeProduct")]
        public ActionResult RemoveDishProduct(
            [FromRoute] Guid dishId,
            [FromQuery] string productId)
        {
            _dishService.RemoveProduct(dishId, productId);
            return NoContent();
        }
    }
}