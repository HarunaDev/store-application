using Microsoft.EntityFrameworkCore;
using StoreApp.Data;
using StoreApp.Models;
using StoreApp.Exceptions;

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

    public async Task<Category> GetCategoryByIdAsync(int id)
    {
        var category = await _context.Categories.FindAsync(id);

        if (category is null)
        {
            throw new NotFoundException("Category not found.");
        }

        return category;
        // return await _context.Categories.FindAsync(id);

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

        // _context.Categories.Add(category);
        // await _context.SaveChangesAsync();

        // _logger.LogInformation(
        //     "Category created. CategoryId={CategoryId}, Name={CategoryName}",
        //     category.Id,
        //     category.Name
        // );
        // return category;

        var exists = await _context.Categories.AnyAsync(c => c.Name.ToLower() == category.Name.ToLower());

        if (exists)
        {
            throw new ConflictException("Category already exists.");
        }

        _context.Categories.Add(category);

        await _context.SaveChangesAsync();

        _logger.LogInformation("Category created. CategoryId={CategoryId}, Name={CategoryName}", category.Id, category.Name);

        return category;
    }
}