using StoreApp.Models;

namespace StoreApp.Services;

public class CategoryService
{
    private readonly List<Category> _categories;
    private readonly ILogger<ProductService> _logger;

    public CategoryService(ILogger<ProductService> logger)
    {
        _logger = logger;
        _categories = new List<Category>
        {
            new Category(1, "Clothing"),
            new Category(2, "Electronics")
        };
    }

    public List<Category> GetCategories()
    {
        return _categories;
    }

    public Category? GetCategoryById(int id)
    {
        var category = _categories.FirstOrDefault(c => c.Id == id);

        if (category is null)
        {
            _logger.LogWarning(
                "Category not found. CategoryId={CategoryId}",
                id
            );

            return null;
        }

        _logger.LogInformation(
            "Category retrieved. CategoryId={CategoryId}, Name={CategoryName}",
            category.Id,
            category.Name
        );

        return category;
    }

    public Category AddCategory(Category category)
    {
        category.Id = _categories.Max(c => c.Id) + 1;

        _categories.Add(category);

        _logger.LogInformation(
            "Category created. CategoryId={CategoryId}, Name={CategoryName}",
            category.Id,
            category.Name
        );
        return category;
    }
}