using NutriApp.Server.Models.Product;

namespace NutriApp.Server.Services.Interfaces
{
    public interface IProductService
    {
        Guid AddProduct(ProductRequest addProductRequest);
        void DeleteProduct(Guid productId);
        ProductDto GetProduct(Guid productId);
        IEnumerable<ProductDto> GetProducts();
        void UpdateProduct(Guid productId, ProductRequest updateProductRequest);
    }
}