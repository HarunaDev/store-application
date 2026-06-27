using System.ComponentModel.DataAnnotations;

namespace StoreApp.DTOs.Category;

public class CategoryDto
{
    public int Id { get; set; }

    public string Name { get; set; } = "";
}

public class CreateCategoryDto
{
    [Required(ErrorMessage = "Category name is required")]
    [MinLength(2, ErrorMessage = "Category name must be at least 2 characters")]
    [MaxLength(100, ErrorMessage = "Category name cannot exceed 100 characters")]
    public string Name { get; set; } = "";
}
