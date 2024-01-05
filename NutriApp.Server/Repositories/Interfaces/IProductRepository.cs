using NutriApp.Server.ApiContract.Models;
using NutriApp.Server.Models;
using NutriApp.Server.Models.Product;

namespace NutriApp.Server.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Guid AddProduct(string userId, ProductRequest addProductRequest);
        void DeleteProduct(string userId, Guid productId);
        ProductDto GetProductById(string userId, Guid productId);
        PageResult<ProductDto> GetUsersProducts(string userId, int pageSize, int pageNumber);
        void UpdateProduct(string userId, Guid productId, ProductRequest updateProductRequest);
        Guid AddApiProduct(FoodById product);
    }
}