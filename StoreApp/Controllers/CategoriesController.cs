using Microsoft.AspNetCore.Mvc;
using StoreApp.DTOs;
using StoreApp.DTOs.Category;
using StoreApp.Models;
using StoreApp.Services;
using Microsoft.AspNetCore.Authorization;
using StoreApp.DTOs.Responses;

namespace StoreApp.Controllers;

[Authorize]
[ApiController]
[ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
[Route("api/categories")]
public class CategoriesController : ControllerBase
{
    private readonly CategoryService _categoryService;

    public CategoriesController(CategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<CategoryDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCategories()
    {
        var categories = await _categoryService.GetCategoriesAsync();

        var categoryDtos = categories.Select(c => new CategoryDto
        {
            Id = c.Id,
            Name = c.Name
        });

        return Ok(new ApiResponse<IEnumerable<CategoryDto>>
        {
            Success = true,
            Message = "Categories retrieved successfully",
            Data = categoryDtos
        });
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<CategoryDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCategory(int id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id);

        var categoryDto = new CategoryDto
        {
            Id = category.Id,
            Name = category.Name
        };
        return Ok(new ApiResponse<CategoryDto>
        {
            Success = true,
            Message = "Category retreived successfully",
            Data = categoryDto
        });
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<CategoryDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> AddCategory(CreateCategoryDto dto)
    {
        var category = new Category(0, dto.Name);

        var createdCategory = await _categoryService.AddCategoryAsync(category);

        // Map Domain Model to DTO
        var categoryDto = new CategoryDto
        {
            Id = createdCategory.Id,
            Name = createdCategory.Name
        };

        return Ok(new ApiResponse<CategoryDto>
        {
            Success = true,
            Message = "Category created successfully",
            Data = categoryDto
        });
    }
}