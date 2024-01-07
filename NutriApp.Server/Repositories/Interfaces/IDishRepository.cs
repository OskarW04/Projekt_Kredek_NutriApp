using NutriApp.Server.Models;
using NutriApp.Server.Models.Dish;

namespace NutriApp.Server.Repositories.Interfaces
{
    public interface IDishRepository
    {
        PageResult<DishDto> GetUsersDishes(string userId, int pageSize, int pageNumber);
        DishDto GetDishById(string userId, Guid dishId);
        Guid AddDish(string userId, DishRequest addDishRequest);
        void DeleteDish(string userId, Guid dishId);
        void UpdateDish(string userId, Guid dishId, DishRequest updateDishRequest);
        Guid AddUserProductToDish(string userId, Guid dishId, Guid productId, uint grams);
        Guid AddApiProductToDish(string userId, Guid dishId, Guid productId, uint grams);
        void RemoveUserProduct(string userId, Guid dishId, Guid productId);
        void RemoveApiProduct(string userId, Guid dishId, string productApiId);
        void UpdateUserProductPortion(string userId, Guid dishId, Guid productId, uint grams);
        void UpdateApiProductPortion(string userId, Guid dishId, string productApiId, uint grams);
    }
}