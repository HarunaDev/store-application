// using StoreApp.Models;

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
    public string Name { get; set; } = "";

    public decimal Price { get; set; }

    public bool HasDiscount { get; set; }

    public int CategoryId { get; set; }

    // Clothing
    public int? Size { get; set; }

    // Electronics
    public bool? Warranty { get; set; }

    public string? Brand { get; set; }

    public IFormFile? ImageFile { get; set; }
}

public class UpdateProductDto
{
    public string Name { get; set; } = "";

    public decimal Price { get; set; }

    public bool HasDiscount { get; set; }

    public int CategoryId { get; set; }

    public int? Size { get; set; }

    public bool? Warranty { get; set; }

    public string? Brand { get; set; }

    public IFormFile? ImageFile { get; set; }
}