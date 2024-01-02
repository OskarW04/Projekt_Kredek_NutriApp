using NutriApp.Server.ApiContract.Models;
using NutriApp.Server.DataAccess.Context;
using NutriApp.Server.DataAccess.Entities.Dishes;
using NutriApp.Server.Exceptions;
using NutriApp.Server.Models;
using NutriApp.Server.Models.Product;
using NutriApp.Server.Repositories.Interfaces;

namespace NutriApp.Server.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _appDbContext;

        public ProductRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Guid AddProduct(string userId, ProductRequest addProductRequest)
        {
            var product = new Product()
            {
                Id = Guid.NewGuid(),
                Name = addProductRequest.Name,
                Brand = addProductRequest.Brand,
                Calories = addProductRequest.Calories,
                Proteins = addProductRequest.Proteins,
                Carbohydrates = addProductRequest.Carbohydrates,
                Fats = addProductRequest.Fats,
                Ingredients = addProductRequest.Ingredients,
                GramsInPortion = addProductRequest.GramsInPortion,
                UserId = userId,
            };

            _appDbContext.Products.Add(product);
            _appDbContext.SaveChanges();

            return product.Id;
        }

        public void DeleteProduct(string userId, Guid productId)
        {
            var product = _appDbContext.Products
                .FirstOrDefault(x => x.Id == productId);

            if (product is null)
            {
                throw new ResourceNotFoundException($"Product with id: {productId} not found");
            }

            if (product.UserId != userId)
            {
                throw new ForbidException("User claims invalid");
            }

            _appDbContext.Products.Remove(product);
            _appDbContext.SaveChanges();
        }

        public ProductDto GetProductById(string userId, Guid productId)
        {
            var product = _appDbContext.Products
                .FirstOrDefault(x => x.Id == productId);

            if (product is null)
            {
                throw new ResourceNotFoundException($"Product with id: {productId} not found");
            }

            if (product.UserId != userId)
            {
                throw new ForbidException("User claims invalid");
            }

            return new ProductDto()
            {
                Id = product.Id,
                Name = product.Name,
                Brand = product.Brand,
                Calories = product.Calories,
                Proteins = product.Proteins,
                Carbohydrates = product.Carbohydrates,
                Fats = product.Fats,
                Ingredients = product.Ingredients,
                GramsInPortion = product.GramsInPortion,
            };
        }

        public PageResult<ProductDto> GetUsersProducts(string userId, int pageSize, int pageNumber)
        {
            var products = _appDbContext.Products
                .Where(x => x.UserId == userId)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToList();

            var results = products
                .Select(product => new ProductDto()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Brand = product.Brand,
                    Calories = product.Calories,
                    Proteins = product.Proteins,
                    Carbohydrates = product.Carbohydrates,
                    Fats = product.Fats,
                    Ingredients = product.Ingredients,
                    GramsInPortion = product.GramsInPortion,
                }).ToList();

            return new PageResult<ProductDto>(
                results,
                results.Count,
                pageSize,
                pageNumber);
        }

        public void UpdateProduct(string userId, Guid productId, ProductRequest updateProductRequest)
        {
            var product = _appDbContext.Products
                .FirstOrDefault(x => x.Id == productId);

            if (product is null)
            {
                throw new ResourceNotFoundException($"Product with id: {productId} not found");
            }

            if (product.UserId != userId)
            {
                throw new ForbidException("User claims invalid");
            }

            product.Name = updateProductRequest.Name;
            product.Brand = updateProductRequest.Brand;
            product.Calories = updateProductRequest.Calories;
            product.Proteins = updateProductRequest.Proteins;
            product.Carbohydrates = updateProductRequest.Carbohydrates;
            product.Fats = updateProductRequest.Fats;
            product.Ingredients = updateProductRequest.Ingredients;
            product.GramsInPortion = updateProductRequest.GramsInPortion;

            _appDbContext.SaveChanges();
        }

        public Guid AddApiProduct(FoodByIdResult.FoodById product)
        {
            var found = _appDbContext.ApiProductInfos
                .FirstOrDefault(x => x.ApiId == product.food_id);

            if (found is not null)
            {
                return found.Id;
            }

            var serving = product.servings.serving
                .FirstOrDefault(x => (x.metric_serving_amount is not null));

            if (serving is not null)
            {
                serving = product.servings.serving
                    .First();
            }

            var portion = 100;
            if (serving?.metric_serving_amount is not null)
            {
                portion = (int)decimal.Parse(serving.metric_serving_amount
                    .Replace(".", ",")
                );
            }

            var productApiUrl = new ApiProductInfo()
            {
                Id = Guid.NewGuid(),
                ApiId = product.food_id,
                Name = product.food_name,
                Brand = product.brand_name,
                Description = serving?.serving_description,
                Portion = serving?.metric_serving_amount,
                Calories = GetValueFromApiParam(serving.calories),
                Proteins = GetValueFromApiParam(serving.protein),
                Carbohydrates = GetValueFromApiParam(serving.carbohydrate),
                Fats = GetValueFromApiParam(serving.fat),
                GramsInPortion = portion,
            };
            return productApiUrl.Id;
        }

        private int GetValueFromApiParam(string text)
        {
            return (int)decimal.Parse(text.Replace(".", ","));
        }
    }
}