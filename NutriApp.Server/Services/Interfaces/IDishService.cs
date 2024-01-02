using NutriApp.Server.Models;
using NutriApp.Server.Models.Dish;

namespace NutriApp.Server.Services.Interfaces
{
    public interface IDishService
    {
        PageResult<DishDto> GetDishes(PaginationParams paginationParams);
        DishDto GetDish(Guid dishId);
        Guid AddDish(DishRequest addDishRequest);
        void DeleteDish(Guid dishId);
        void UpdateDish(Guid dishId, DishRequest updateDishRequest);
        Task<string> AddProduct(Guid dishId, string productId, uint grams);
        void UpdatePortion(Guid dishId, string productId, uint grams);
        void RemoveProduct(Guid dishId, string productId);
    }
}