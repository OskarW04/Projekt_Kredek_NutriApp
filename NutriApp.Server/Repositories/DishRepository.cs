using Microsoft.EntityFrameworkCore;
using NutriApp.Server.DataAccess.Context;
using NutriApp.Server.DataAccess.Entities.Dishes;
using NutriApp.Server.Exceptions;
using NutriApp.Server.Models;
using NutriApp.Server.Models.Dish;
using NutriApp.Server.Models.Product;
using NutriApp.Server.Repositories.Interfaces;

namespace NutriApp.Server.Repositories
{
    public class DishRepository : IDishRepository
    {
        private readonly AppDbContext _dbContext;

        public DishRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public PageResult<DishDto> GetUsersDishes(string userId, int pageSize,
            int pageNumber)
        {
            var dishes = _dbContext.Dishes
                .Include(x => x.DishApiProducts)!
                .ThenInclude(x => x.ApiProductInfo)
                .Include(x => x.DishProducts)!
                .ThenInclude(x => x.Product)
                .Where(x => x.UserId == userId)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToList();

            var results = dishes.Select(dish =>
            {
                return new DishDto()
                {
                    Id = dish.Id,
                    Name = dish.Name,
                    Description = dish.Description,
                    GramsTotal = dish.GramsTotal,
                    Calories = dish.Calories,
                    Proteins = dish.Proteins,
                    Carbohydrates = dish.Carbohydrates,
                    Fats = dish.Fats,
                    DishProducts = dish.DishProducts.Select(dishProducts => new ProductDto()
                    {
                        Id = dishProducts.Product.Id,
                        Name = dishProducts.Product.Name,
                        Brand = dishProducts.Product.Brand,
                        Calories = dishProducts.Product.Calories,
                        Proteins = dishProducts.Product.Proteins,
                        Carbohydrates = dishProducts.Product.Carbohydrates,
                        Fats = dishProducts.Product.Fats,
                        Ingredients = dishProducts.Product.Ingredients,
                        GramsInPortion = dishProducts.Product.GramsInPortion,
                    }).ToList(),
                    DishApiProducts = dish.DishApiProducts.Select(apiProducts => new ApiProductDto()
                    {
                        ApiUrl = apiProducts.ApiProductInfo.ApiUrl,
                        ApiId = apiProducts.ApiProductInfo.ApiId,
                        Name = apiProducts.ApiProductInfo.Name,
                        Brand = apiProducts.ApiProductInfo.Brand,
                        Description = apiProducts.ApiProductInfo.Description,
                        Portion = apiProducts.ApiProductInfo.Portion,
                        Calories = apiProducts.ApiProductInfo.Calories,
                        Proteins = apiProducts.ApiProductInfo.Proteins,
                        Carbohydrates = apiProducts.ApiProductInfo.Carbohydrates,
                        Fats = apiProducts.ApiProductInfo.Fats,
                        GramsInPortion = apiProducts.ApiProductInfo.GramsInPortion,
                    }).ToList(),
                };
            }).ToList();

            return new PageResult<DishDto>(
                results,
                results.Count,
                pageSize,
                pageNumber);
        }

        public DishDto GetDishById(string userId, Guid dishId)
        {
            var dish = _dbContext.Dishes
                .Include(x => x.DishApiProducts)!
                .ThenInclude(x => x.ApiProductInfo)
                .Include(x => x.DishProducts)!
                .ThenInclude(x => x.Product)
                .FirstOrDefault(x => x.Id == dishId);

            if (dish is null)
            {
                throw new ResourceNotFoundException($"Dish with id: {dishId} not found");
            }

            if (dish.UserId != userId)
            {
                throw new ForbidException("User claims invalid");
            }

            return new DishDto()
            {
                Id = dish.Id,
                Name = dish.Name,
                Description = dish.Description,
                GramsTotal = dish.GramsTotal,
                Calories = dish.Calories,
                Proteins = dish.Proteins,
                Carbohydrates = dish.Carbohydrates,
                Fats = dish.Fats,
                DishProducts = dish.DishProducts.Select(dishProducts => new ProductDto()
                {
                    Id = dishProducts.Product.Id,
                    Name = dishProducts.Product.Name,
                    Brand = dishProducts.Product.Brand,
                    Calories = dishProducts.Product.Calories,
                    Proteins = dishProducts.Product.Proteins,
                    Carbohydrates = dishProducts.Product.Carbohydrates,
                    Fats = dishProducts.Product.Fats,
                    Ingredients = dishProducts.Product.Ingredients,
                    GramsInPortion = dishProducts.Product.GramsInPortion,
                }).ToList(),
                DishApiProducts = dish.DishApiProducts.Select(apiProducts => new ApiProductDto()
                {
                    ApiUrl = apiProducts.ApiProductInfo.ApiUrl,
                    ApiId = apiProducts.ApiProductInfo.ApiId,
                    Name = apiProducts.ApiProductInfo.Name,
                    Brand = apiProducts.ApiProductInfo.Brand,
                    Description = apiProducts.ApiProductInfo.Description,
                    Portion = apiProducts.ApiProductInfo.Portion,
                    Calories = apiProducts.ApiProductInfo.Calories,
                    Proteins = apiProducts.ApiProductInfo.Proteins,
                    Carbohydrates = apiProducts.ApiProductInfo.Carbohydrates,
                    Fats = apiProducts.ApiProductInfo.Fats,
                    GramsInPortion = apiProducts.ApiProductInfo.GramsInPortion,
                }).ToList(),
            };
        }

        public Guid AddDish(string userId, DishRequest addDishRequest)
        {
            var dish = new Dish()
            {
                Id = Guid.NewGuid(),
                Name = addDishRequest.Name,
                Description = addDishRequest.Description,
                UserId = userId,
            };

            _dbContext.Dishes.Add(dish);
            _dbContext.SaveChanges();

            return dish.Id;
        }

        public void DeleteDish(string userId, Guid dishId)
        {
            var dish = _dbContext.Dishes
                .FirstOrDefault(x => x.Id == dishId);

            if (dish is null)
            {
                throw new ResourceNotFoundException($"Dish with id: {dishId} not found");
            }

            if (dish.UserId != userId)
            {
                throw new ForbidException("User claims invalid");
            }

            _dbContext.Dishes.Remove(dish);
            _dbContext.SaveChanges();
        }

        public void UpdateDish(string userId, Guid dishId, DishRequest updateDishRequest)
        {
            var dish = _dbContext.Dishes
                .FirstOrDefault(x => x.Id == dishId);

            if (dish is null)
            {
                throw new ResourceNotFoundException($"Dish with id: {dishId} not found");
            }

            if (dish.UserId != userId)
            {
                throw new ForbidException("User claims invalid");
            }

            dish.Name = updateDishRequest.Name;
            if (updateDishRequest.Description is not null)
            {
                dish.Description = updateDishRequest.Description;
            }

            _dbContext.SaveChanges();
        }

        public Guid AddUserProductToDish(string userId, Guid dishId, Guid productId, uint grams)
        {
            var dish = _dbContext.Dishes
                .Include(x => x.DishProducts)
                .FirstOrDefault(x => x.Id == dishId);

            if (dish is null)
            {
                throw new ResourceNotFoundException($"Dish with id: {dishId} not found");
            }

            if (dish.UserId != userId)
            {
                throw new ForbidException("User claims invalid");
            }

            var product = _dbContext.Products
                .FirstOrDefault(x => x.Id == productId);

            if (product is null)
            {
                throw new ResourceNotFoundException($"Product with id: {productId} not found");
            }

            if (product.UserId != userId)
            {
                throw new ForbidException("User claims invalid");
            }

            var dishProduct = new DishProducts()
            {
                Id = Guid.NewGuid(),
                DishId = dishId,
                ProductId = productId,
                Amount = (int)grams,
            };

            dish.GramsTotal += (int)grams;
            dish.Calories += (int)(product.Calories * grams / product.GramsInPortion);
            dish.Proteins += (int)(product.Proteins * grams / product.GramsInPortion);
            dish.Carbohydrates += (int)(product.Carbohydrates * grams / product.GramsInPortion);
            dish.Fats += (int)(product.Fats * grams / product.GramsInPortion);

            _dbContext.DishProducts.Add(dishProduct);
            _dbContext.SaveChanges();

            return dishProduct.Id;
        }

        public Guid AddApiProductToDish(string userId, Guid dishId, Guid productId, uint grams)
        {
            var dish = _dbContext.Dishes
                .Include(x => x.DishProducts)
                .FirstOrDefault(x => x.Id == dishId);

            if (dish is null)
            {
                throw new ResourceNotFoundException($"Dish with id: {dishId} not found");
            }

            if (dish.UserId != userId)
            {
                throw new ForbidException("User claims invalid");
            }

            var product = _dbContext.ApiProductInfos
                .FirstOrDefault(x => x.Id == productId);

            if (product is null)
            {
                throw new ResourceNotFoundException($"Product with id: {productId} not found");
            }

            var dishProduct = new DishApiProducts()
            {
                Id = Guid.NewGuid(),
                DishId = dishId,
                ApiProductInfoId = productId,
                Amount = (int)grams,
            };

            dish.GramsTotal += (int)grams;
            dish.Calories += (int)(product.Calories * grams / product.GramsInPortion);
            dish.Proteins += (int)(product.Proteins * grams / product.GramsInPortion);
            dish.Carbohydrates += (int)(product.Carbohydrates * grams / product.GramsInPortion);
            dish.Fats += (int)(product.Fats * grams / product.GramsInPortion);

            _dbContext.DishApiProducts?.Add(dishProduct);
            _dbContext.SaveChanges();

            return dishProduct.Id;
        }
    }
}