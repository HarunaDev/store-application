using System.ComponentModel.DataAnnotations;
namespace StoreApp.DTOs.Product;

public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; } = "";

    public decimal Price { get; set; }

    public bool HasDiscount { get; set; }

    public int CategoryId { get; set; }

    // Clothing
    public int? Size { get; set; }

    // Electronics
    public bool? Warranty { get; set; }

    public string? Brand { get; set; }

    public string? ImageUrl { get; set; }
}

public class CreateProductDto
{
    [MinLength(2, ErrorMessage = "Product name must be at least 2 characters")]
    [MaxLength(100, ErrorMessage = "Product name cannot exceed 100 characters")]
    public required string Name { get; set; } = "";

    [Range(0.01, double.MaxValue,
        ErrorMessage = "Price must be greater than zero")]
    public decimal Price { get; set; }

    public bool HasDiscount { get; set; }

    [Range(1, int.MaxValue,
        ErrorMessage = "A valid category is required")]
    public int CategoryId { get; set; }

    // Clothing
    [Range(1, 100,
        ErrorMessage = "Size must be between 1 and 100")]
    public int? Size { get; set; }

    // Electronics
    [MaxLength(100,
        ErrorMessage = "Brand cannot exceed 100 characters")]
    public string? Brand { get; set; }

    public bool? Warranty { get; set; }

    public IFormFile? ImageFile { get; set; }
}


public class UpdateProductDto
{
    [MinLength(2, ErrorMessage = "Product name must be at least 2 characters")]
    [MaxLength(100, ErrorMessage = "Product name cannot exceed 100 characters")]
    public required string Name { get; set; }

    [Range(0.01, double.MaxValue,
        ErrorMessage = "Price must be greater than zero")]
    public decimal Price { get; set; }

    public bool HasDiscount { get; set; }

    [Range(1, int.MaxValue,
        ErrorMessage = "A valid category is required")]
    public int CategoryId { get; set; }

    [Range(1, 100,
        ErrorMessage = "Size must be between 1 and 100")]
    public int? Size { get; set; }

    public bool? Warranty { get; set; }

    [MaxLength(100,
        ErrorMessage = "Brand cannot exceed 100 characters")]
    public string? Brand { get; set; }

    public IFormFile? ImageFile { get; set; }
}