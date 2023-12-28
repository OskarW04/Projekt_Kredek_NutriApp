using NutriApp.Server.Models.Product;

namespace NutriApp.Server.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Guid AddProduct(string userId, ProductRequest addProductRequest);
        void DeleteProduct(string userId, Guid productId);
        ProductDto GetProductById(string userId, Guid productId);
        IEnumerable<ProductDto> GetUsersProducts(string userId);
        void UpdateProduct(string userId, Guid productId, ProductRequest updateProductRequest);
    }
}