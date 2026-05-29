namespace StoreApp.DTOs;

public class CreateProductDto
{
    public string Name { get; set; } = "";

    public decimal Price { get; set; }

    public bool HasDiscount { get; set; }

    public string ProductType { get; set; } = "";

    public int CategoryId { get; set; }

    // Clothing
    public int? Size { get; set; }

    // Electronics
    public bool? Warranty { get; set; }

    public string? Brand { get; set; }
}