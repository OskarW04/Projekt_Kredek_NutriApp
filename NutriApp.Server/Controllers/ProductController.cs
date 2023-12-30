﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NutriApp.Server.Models.Product;
using NutriApp.Server.Services.Interfaces;

namespace NutriApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("userProducts")]
        public ActionResult<IEnumerable<ProductDto>> GetUsersProducts()
        {
            var products = _productService.GetProducts();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public ActionResult<ProductDto> GetProduct([FromRoute] Guid id)
        {
            var product = _productService.GetProduct(id);
            return Ok(product);
        }

        [HttpPost]
        public ActionResult AddProduct([FromBody] ProductRequest addProductRequest)
        {
            var productId = _productService.AddProduct(addProductRequest);
            return Created($"/api/product/{productId}", null);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateProduct([FromRoute] Guid id, [FromBody] ProductRequest updateProductRequest)
        {
            _productService.UpdateProduct(id, updateProductRequest);
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] Guid id)
        {
            _productService.DeleteProduct(id);
            return NoContent();
        }
    }
}