using NutriApp.Server.Exceptions;
using NutriApp.Server.Models.Product;
using NutriApp.Server.Repositories.Interfaces;
using NutriApp.Server.Services.Interfaces;

namespace NutriApp.Server.Services
{
    public class ProductService : IProductService
    {
        IProductRepository _productRepository;
        IUserContextService _userContextService;

        public ProductService(IProductRepository productRepository, IUserContextService userContextService)
        {
            _productRepository = productRepository;
            _userContextService = userContextService;
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

        public IEnumerable<ProductDto> GetProducts()
        {
            var userId = VerifyUserClaims();
            return _productRepository.GetUsersProducts(userId);
        }

        public void UpdateProduct(Guid productId, ProductRequest updateProductRequest)
        {
            var userId = VerifyUserClaims();
            _productRepository.UpdateProduct(userId, productId, updateProductRequest);
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