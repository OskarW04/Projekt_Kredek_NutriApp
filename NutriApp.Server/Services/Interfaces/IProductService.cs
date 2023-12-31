﻿using NutriApp.Server.Models;
using NutriApp.Server.Models.Product;

namespace NutriApp.Server.Services.Interfaces
{
    public interface IProductService
    {
        Guid AddProduct(ProductRequest addProductRequest);
        void DeleteProduct(Guid productId);
        ProductDto GetProduct(Guid productId);
        PageResult<ProductDto> GetProducts(PaginationParams paginationParams);
        void UpdateProduct(Guid productId, ProductRequest updateProductRequest);
        Task<PageResult<ApiProductDto>> GetApiProducts(int pageNumber, int pageSize, string search);
    }
}