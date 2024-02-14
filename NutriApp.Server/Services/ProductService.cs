using NutriApp.Server.ApiContract;
using NutriApp.Server.Exceptions;
using NutriApp.Server.Models;
using NutriApp.Server.Models.Product;
using NutriApp.Server.Repositories.Interfaces;
using NutriApp.Server.Services.Interfaces;

namespace NutriApp.Server.Services
{
    public class ProductService : IProductService
    {
        private readonly FoodApiSearchService _foodApiSearchService;
        private readonly IProductRepository _productRepository;
        private readonly IUserContextService _userContextService;

        public ProductService(IProductRepository productRepository, IUserContextService userContextService,
            FoodApiSearchService foodApiSearchService)
        {
            _productRepository = productRepository;
            _userContextService = userContextService;
            _foodApiSearchService = foodApiSearchService;
        }

        public Guid AddProduct(ProductRequest addProductRequest)
        {
            var userId = VerifyUserClaims();
            return _productRepository.AddProduct(userId, addProductRequest);
        }

        public void DeleteProduct(Guid productId)
        {
            var userId = VerifyUserClaims();
            _productRepository.DeleteProduct(userId, productId);
        }

        public ProductDto GetProduct(Guid productId)
        {
            var userId = VerifyUserClaims();
            return _productRepository.GetProductById(userId, productId);
        }

        public PageResult<ProductDto> GetProducts(PaginationParams paginationParams)
        {
            var userId = VerifyUserClaims();
            return _productRepository.GetUsersProducts(userId, paginationParams.PageSize, paginationParams.PageNumber);
        }

        public void UpdateProduct(Guid productId, ProductRequest updateProductRequest)
        {
            var userId = VerifyUserClaims();
            _productRepository.UpdateProduct(userId, productId, updateProductRequest);
        }

        public async Task<PageResult<ApiProductDto>> GetApiProducts(int pageNumber, int pageSize, string search)
        {
            VerifyUserClaims();
            var result = await _foodApiSearchService.FetchFoodSearch(search, pageNumber, pageSize);

            if (result.Food is null || result.Food.Count == 0)
            {
                return new PageResult<ApiProductDto>(new List<ApiProductDto>(), 0, pageSize, pageNumber);
            }

            var isResultCount = int.TryParse(result.TotalResults, out var parsedResultCount);
            var totalItemsCount = isResultCount ? parsedResultCount : result.Food.Count;

            return new PageResult<ApiProductDto>(
                result.Food.Select(x =>
                {
                    var portionSplit = x.FoodDescription.Split("-");
                    var portionInfo = portionSplit[0].Remove(portionSplit[0].Length - 1);

                    return new ApiProductDto
                    {
                        ApiUrl = x.FoodUrl,
                        ApiId = x.FoodId,
                        Name = x.FoodName,
                        Brand = x.BrandName,
                        Description = x.FoodDescription,
                        Portion = portionInfo,
                        Calories = GetValueFromApiProductDescription(x.FoodDescription, "Calories: ", "kcal"),
                        Proteins = GetValueFromApiProductDescription(x.FoodDescription, "Protein: ", "g"),
                        Carbohydrates = GetValueFromApiProductDescription(x.FoodDescription, "Carbs: ", "g"),
                        Fats = GetValueFromApiProductDescription(x.FoodDescription, "Fat: ", "g")
                    };
                }),
                totalItemsCount,
                pageSize,
                pageNumber
            );
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

        private static int GetValueFromApiProductDescription(string text, string firstSeparator, string secondSeparator)
        {
            var stringValue = text.Split([firstSeparator], StringSplitOptions.None)[1]
                .Split(secondSeparator)[0]
                .Trim();

            var success = decimal.TryParse(stringValue, System.Globalization.NumberStyles.AllowDecimalPoint,
                System.Globalization.CultureInfo.InvariantCulture, out var result);

            if (!success)
            {
                return 0;
            }

            return (int)result;
        }
    }
}