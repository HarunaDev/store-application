using Microsoft.AspNetCore.Mvc;
using StoreApp.DTOs;
using StoreApp.Models;
using StoreApp.Services;

namespace StoreApp.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoriesController : ControllerBase
{
    private readonly CategoryService _categoryService;

    public CategoriesController(CategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public IActionResult GetCategories()
    {
        return Ok(_categoryService.GetCategories());
    }

    [HttpGet("{id}")]
    public IActionResult GetCategory(int id)
    {
        var category = _categoryService.GetCategoryById(id);

        if (category is null)
        {
            return NotFound();
        }
        return Ok(category);
    }

    [HttpPost]
    public IActionResult AddCategory(CreateCategoryDto dto)
    {
        var category = new Category(0, dto.Name);

        var createdCategory =
            _categoryService.AddCategory(category);

        return Ok(createdCategory);
    }
}