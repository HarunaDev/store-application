using Microsoft.EntityFrameworkCore;
using StoreApp.Data;
using StoreApp.Models;

namespace StoreApp.Services;

public class CategoryService
{
    private readonly StoreAppDbContext _context;
    // private readonly List<Category> _categories;
    private readonly ILogger<CategoryService> _logger;

    public CategoryService(StoreAppDbContext context, ILogger<CategoryService> logger)
    {
        _context = context;
        _logger = logger;
        // _categories = new List<Category>
        // {
        //     new Category(1, "Clothing"),
        //     new Category(2, "Electronics")
        // };
    }



    public async Task<List<Category>> GetCategoriesAsync()
    {
        return await _context.Categories.ToListAsync();
    }

    public async Task<Category?> GetCategoryByIdAsync(int id)
    {
        return await _context.Categories.FindAsync(id);

        // if (category is null)
        // {
        //     _logger.LogWarning(
        //         "Category not found. CategoryId={CategoryId}",
        //         id
        //     );

        //     return null;
        // }

        // _logger.LogInformation(
        //     "Category retrieved. CategoryId={CategoryId}, Name={CategoryName}",
        //     category.Id,
        //     category.Name
        // );

        // return category;
    }

    public async Task<Category> AddCategoryAsync(Category category)
    {
        // category.Id = _categories.Max(c => c.Id) + 1;

        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        _logger.LogInformation(
            "Category created. CategoryId={CategoryId}, Name={CategoryName}",
            category.Id,
            category.Name
        );
        return category;
    }
}