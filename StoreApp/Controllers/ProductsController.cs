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
    [ProducesResponseType(typeof(ApiResponse<ProductPagedResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProducts([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var (items, meta) = await _productService.GetProductsAsync(pageNumber, pageSize);

        // var pagedProducts = await _productService.GetProductsAsync(pageNumber, pageSize);

        var productDtos = items.Select(p => new ProductDto
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
            HasDiscount = p.HasDiscount,
            CategoryId = p.CategoryId,
            Size = p.Size ?? 0,
            Warranty = p.Warranty ?? false,
            Brand = p.Brand ?? string.Empty,
            ImageUrl = p.ImageUrl
        });

        var response = new ProductPagedResponse
        {
            PageNumber = meta.PageNumber,
            PageSize = meta.PageSize,
            TotalRecords = meta.TotalRecords,
            Products = productDtos
        };

        return Ok(new ApiResponse<ProductPagedResponse>
        {
            Success = true,
            Message = "Products retrieved successfully",
            Data = response
        });
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<ProductDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProduct(int id)
    {
        var product = await _productService.GetProductByIdAsync(id);

        var productDto = new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            HasDiscount = product.HasDiscount,
            CategoryId = product.CategoryId,
            Size = product.Size,
            Warranty = product.Warranty,
            Brand = product.Brand,
            ImageUrl = product.ImageUrl
        };

        return Ok(new ApiResponse<ProductDto>
        {
            Success = true,
            Message = "Product retrieved successfully",
            Data = productDto
        });
    }

    [HttpPost]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<ProductDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> AddProduct([FromForm] CreateProductDto dto)
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
            Brand = product?.Brand,
            ImageUrl = product.ImageUrl
        };

        return Ok(new ApiResponse<ProductDto>
        {
            Success = true,
            Message = "Product created successfully",
            Data = productDto
        });
    }

    [HttpPut("{id}")]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(ApiResponse<ProductDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateProduct(
        int id,
        [FromForm] UpdateProductDto dto
    )
    {
        var updatedProduct = await _productService.UpdateProductAsync(
            id,
            dto
        );

        var productDto = new ProductDto
        {
            Id = updatedProduct.Id,
            Name = updatedProduct.Name,
            Price = updatedProduct.Price,
            HasDiscount = updatedProduct.HasDiscount,
            CategoryId = updatedProduct.CategoryId,
            Size = updatedProduct.Size,
            Warranty = updatedProduct.Warranty,
            Brand = updatedProduct.Brand,
            ImageUrl = updatedProduct.ImageUrl
        };

        return Ok(new ApiResponse<ProductDto>
        {
            Success = true,
            Message = "Product updated successfully",
            Data = productDto
        });
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse<object?>), StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        await _productService.DeleteProductAsync(id);

        return Ok(new ApiResponse<object?>
        {
            Success = true,
            Message = "Product deleted successfully",
            Data = null
        });
    }
}