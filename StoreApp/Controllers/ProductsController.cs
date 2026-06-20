using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.RateLimiting;
using StoreApp.DTOs;
using StoreApp.DTOs.Product;
using StoreApp.Models;
using StoreApp.DTOs.Responses;
using StoreApp.Services;
using System.Drawing;



namespace StoreApp.Controllers;

[Authorize]
[EnableRateLimiting("ApiPolicy")]
[ApiController]
[ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly ProductService _productService;

    public ProductsController(ProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<ProductDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProducts()
    {
        try
        {
            var products = await _productService.GetProductsAsync();

            var productDtos = products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                HasDiscount = p.HasDiscount,
                CategoryId = p.CategoryId,
                Size = p?.Size,
                Warranty = p?.Warranty,
                Brand = p?.Brand
            });

            return Ok(new ApiResponse<IEnumerable<ProductDto>>
            {
                Success = true,
                Message = "Products retrieved successfully",
                Data = productDtos
            });

        }
        catch (Exception)
        {
            return BadRequest(new ErrorResponse
            {
                ErrorCode = "FETCH_PRODUCTS_FAILED",
                Message = "Unable to retreive products"
            });
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<ProductDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProduct(int id)
    {
        try
        {
            var product = await _productService.GetProductByIdAsync(id);

            if (product is null)
            {
                return NotFound();
            }

            var productDto = new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                HasDiscount = product.HasDiscount,
                CategoryId = product.CategoryId
            };

            return Ok(new ApiResponse<ProductDto>
            {
                Success = true,
                Message = "Product retrieved successfully",
                Data = productDto
            });
        }
        catch (Exception)
        {
            return BadRequest(new ErrorResponse
            {
                ErrorCode = "FETCH_PRODUCT_FAILED",
                Message = "Unable to retreive product"
            });
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<ProductDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> AddProduct([FromBody] CreateProductDto dto)
    {
        try
        {
            var product = await _productService.AddProductAsync(dto);

            var productDto = new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                HasDiscount = product.HasDiscount,
                CategoryId = product.CategoryId,
                Size = product?.Size,
                Warranty = product?.Warranty,
                Brand = product?.Brand
            };

            return Ok(new ApiResponse<ProductDto>
            {
                Success = true,
                Message = "Product created successfully",
                Data = productDto
            });
        }
        catch (Exception)
        {
            return BadRequest(new ErrorResponse
            {
                ErrorCode = "CREATE_PRODUCT_FAILED",
                Message = "Unable to create product"
            });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(
        int id,
        Product updatedProduct
    )
    {
        var updated = await _productService.UpdateProductAsync(
            id,
            updatedProduct
        );

        if (!updated)
        {
            return NotFound();
        }

        return Ok(updatedProduct);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var deleted = await _productService.DeleteProductAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}