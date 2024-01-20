using NutriApp.Server.ApiContract;
using NutriApp.Server.Exceptions;
using NutriApp.Server.Models;
using NutriApp.Server.Models.Dish;
using NutriApp.Server.Repositories.Interfaces;
using NutriApp.Server.Services.Interfaces;

namespace NutriApp.Server.Services
{
    public class DishService : IDishService
    {
        private readonly FoodApiSearchService _foodApiSearchService;
        private readonly IUserContextService _userContextService;
        private readonly IDishRepository _dishRepository;
        private readonly IProductRepository _productRepository;

        public DishService(IDishRepository dishRepository,
            FoodApiSearchService foodApiSearchService,
            IUserContextService userContextService, IProductRepository productRepository)
        {
            _dishRepository = dishRepository;
            _foodApiSearchService = foodApiSearchService;
            _userContextService = userContextService;
            _productRepository = productRepository;
        }

        public PageResult<DishDto> GetDishes(PaginationParams paginationParams)
        {
            var userId = VerifyUserClaims();
            return _dishRepository.GetUsersDishes(userId, paginationParams.PageSize, paginationParams.PageNumber);
        }

        public DishDto GetDish(Guid dishId)
        {
            var userId = VerifyUserClaims();
            return _dishRepository.GetDishById(userId, dishId);
        }

        public Guid AddDish(DishRequest addDishRequest)
        {
            var userId = VerifyUserClaims();
            return _dishRepository.AddDish(userId, addDishRequest);
        }

        public void DeleteDish(Guid dishId)
        {
            var userId = VerifyUserClaims();
            _dishRepository.DeleteDish(userId, dishId);
        }

        public void UpdateDish(Guid dishId, DishRequest updateDishRequest)
        {
            var userId = VerifyUserClaims();
            _dishRepository.UpdateDish(userId, dishId, updateDishRequest);
        }

        public async Task<string> AddProduct(Guid dishId, string productId, uint grams)
        {
            var userId = VerifyUserClaims();

            var result = Guid.TryParse(productId, out var parsedId);
            if (!result)
            {
                var product = await _foodApiSearchService.FetchFoodByApiId(productId);
                if (product is null)
                {
                    throw new ResourceNotFoundException($"Product with id: {productId} not found");
                }

                var apiProductId = _productRepository.AddApiProduct(product);
                var apiProductDishId = _dishRepository.AddApiProductToDish(userId, dishId, apiProductId, grams);
                return apiProductDishId.ToString();
            }

            var productDishId = _dishRepository.AddUserProductToDish(userId, dishId, parsedId, grams);
            return productDishId.ToString();
        }

        public void UpdatePortion(Guid dishId, string productId, uint grams)
        {
            var userId = VerifyUserClaims();
            var result = Guid.TryParse(productId, out var parsedId);
            if (!result)
            {
                _dishRepository.UpdateApiProductPortion(userId, dishId, productId, grams);
            }

            _dishRepository.UpdateUserProductPortion(userId, dishId, parsedId, grams);
        }

        public void RemoveProduct(Guid dishId, string productId)
        {
            var userId = VerifyUserClaims();

            var result = Guid.TryParse(productId, out var parsedId);
            if (!result)
            {
                _dishRepository.RemoveApiProduct(userId, dishId, productId);
                return;
            }

            _dishRepository.RemoveUserProduct(userId, dishId, parsedId);
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