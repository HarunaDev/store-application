using StoreApp.Models;

namespace StoreApp.Services;

public class CategoryService
{
    private readonly List<Category> _categories;

    public CategoryService()
    {
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
        return _categories.FirstOrDefault(c => c.Id == id);
    }

    public Category AddCategory(Category category)
    {
        category.Id = _categories.Max(c => c.Id) + 1;

        _categories.Add(category);

        return category;
    }
}