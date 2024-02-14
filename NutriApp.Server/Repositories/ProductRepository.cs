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
            var baseQuery = _appDbContext.Products
                .Where(x => x.UserId == userId);

            var products = baseQuery
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
                baseQuery.Count(),
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

        public Guid AddApiProduct(FoodById product)
        {
            var found = _appDbContext.ApiProductInfos
                .FirstOrDefault(x => x.ApiId == product.FoodId);

            if (found is not null)
            {
                return found.Id;
            }

            var serving = product.Servings.Serving
                .FirstOrDefault(x => x.MetricServingAmount is not null);
            if (serving is null)
            {
                serving = product.Servings.Serving
                    .First();
            }

            int portion;
            var proteins = GetValueFromApiParam(serving.Protein);
            var carbohydrates = GetValueFromApiParam(serving.Carbohydrate);
            var fats = GetValueFromApiParam(serving.Fat);
            if (serving.MetricServingAmount is not null)
            {
                portion = GetValueFromApiParam(serving.MetricServingAmount);
            }
            else
            {
                portion = proteins + carbohydrates + fats;
            }

            var productApiUrl = new ApiProductInfo()
            {
                Id = Guid.NewGuid(),
                ApiUrl = product.FoodUrl,
                ApiId = product.FoodId,
                Name = product.FoodName,
                Brand = product.BrandName,
                Description = serving.ServingDescription,
                Portion = serving.MetricServingAmount,
                Calories = GetValueFromApiParam(serving.Calories),
                Proteins = proteins,
                Carbohydrates = carbohydrates,
                Fats = fats,
                GramsInPortion = portion,
            };

            _appDbContext.ApiProductInfos.Add(productApiUrl);
            _appDbContext.SaveChanges();

            return productApiUrl.Id;
        }

        private static int GetValueFromApiParam(string? text)
        {
            var success = decimal.TryParse(text, System.Globalization.NumberStyles.AllowDecimalPoint,
                System.Globalization.CultureInfo.InvariantCulture, out var result);
            if (!success)
            {
                return 0;
            }

            return (int)result;
        }
    }
}