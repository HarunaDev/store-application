using StoreApp.DTOs;
using StoreApp.Models;

namespace StoreApp.Services;

public class ProductService
{
    private readonly List<Product> _products;

    private readonly CategoryService _categoryService;

    private readonly ILogger<ProductService> _logger;

    public ProductService(CategoryService categoryService, ILogger<ProductService> logger)
    {
        _categoryService = categoryService;
        _logger = logger;
        _products = new List<Product>
        {
            new Clothing(
                1,
                "Samo Vintage Shirt",
                49.99m,
                2,
                true,
                1
            ),

            new Electronics(
                2,
                "Television",
                334.55m,
                true,
                "Philips",
                true,
                2
            )
        };
    }

    public List<Product> GetProducts()
    {
        return _products;
    }

    public Product? GetProductById(int id)
    {
        return _products.FirstOrDefault(p => p.Id == id);
    }

    public Product AddProduct(CreateProductDto dto)
    {
        int nextId = _products.Any()
        ? _products.Max(p => p.Id) + 1
        : 1;

        var category = _categoryService.GetCategoryById(dto.CategoryId);

        if (category is null)
            throw new Exception("Invalid category");

        var product = new Product(nextId, dto.Name, dto.Price, dto.HasDiscount, dto.CategoryId);

        product.Category = category;
        _logger.LogInformation(
                    "Creating product. Name={Name}, CategoryId={CategoryId}",
                    dto.Name,
                    dto.CategoryId
                );
        _products.Add(product);


        return product;
    }

    public bool UpdateProduct(int id, Product updatedProduct)
    {
        var existingProduct = GetProductById(id);

        if (existingProduct is null)
        {
            _logger.LogWarning(
                "Update failed. Product {ProductId} not found",
                id
            );
            return false;
        }

        existingProduct.Name = updatedProduct.Name;
        existingProduct.Price = updatedProduct.Price;
        existingProduct.HasDiscount = updatedProduct.HasDiscount;

        _logger.LogInformation(
            "Updating product {ProductId}",
            id
        );

        return true;
    }

    public bool DeleteProduct(int id)
    {
        var product = GetProductById(id);

        if (product is null)
        {
            _logger.LogWarning(
                "Delete failed. Product {ProductId} not found",
                id
            );
            return false;
        }

        _products.Remove(product);
        _logger.LogInformation(
            "Deleting product {ProductId}",
            id
        );

        return true;
    }
}