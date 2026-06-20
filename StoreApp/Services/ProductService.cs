using Microsoft.EntityFrameworkCore;
using StoreApp.Data;
using StoreApp.DTOs;
using StoreApp.DTOs.Product;
using StoreApp.Models;

namespace StoreApp.Services;

public class ProductService
{
    // private readonly List<Product> _products;
    private readonly StoreAppDbContext _context;

    private readonly CategoryService _categoryService;

    private readonly ILogger<ProductService> _logger;

    public ProductService(StoreAppDbContext context, CategoryService categoryService, ILogger<ProductService> logger)
    {
        _context = context;
        _categoryService = categoryService;
        _logger = logger;
        // _products = new List<Product>
        // {
        //     new Clothing(
        //         1,
        //         "Samo Vintage Shirt",
        //         49.99m,
        //         2,
        //         true,
        //         1
        //     ),

        //     new Electronics(
        //         2,
        //         "Television",
        //         334.55m,
        //         true,
        //         "Philips",
        //         true,
        //         2
        //     )
        // };
    }

    public async Task<List<Product>> GetProductsAsync()
    {
        return await _context.Products.Include(p => p.Category).ToListAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
        // return await _context.Products.FindAsync(id);
    }

    public async Task<Product> AddProductAsync(CreateProductDto dto)
    {
        // int nextId = _products.Any()
        // ? _products.Max(p => p.Id) + 1
        // : 1;


        var category = await _categoryService.GetCategoryByIdAsync(dto.CategoryId);

        if (category is null)
            throw new Exception("Invalid category");

        var product = new Product
        {
            Name = dto.Name,
            Price = dto.Price,
            HasDiscount = dto.HasDiscount,
            CategoryId = dto.CategoryId,
            Category = category,

            Size = dto.Size,
            Warranty = dto.Warranty,
            Brand = dto.Brand
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        _logger.LogInformation(
                    "Creating product. Name={Name}, CategoryId={CategoryId}",
                    dto.Name,
                    dto.CategoryId
                );
        // _products.Add(product);


        return product;
    }

    public async Task<bool> UpdateProductAsync(int id, Product updatedProduct)
    {
        var existingProduct = await GetProductByIdAsync(id);

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

        await _context.SaveChangesAsync();
        _logger.LogInformation(
            "Updating product {ProductId}",
            id
        );

        return true;
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        var product = await GetProductByIdAsync(id);

        if (product is null)
        {
            _logger.LogWarning(
                "Delete failed. Product {ProductId} not found",
                id
            );
            return false;
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        _logger.LogInformation(
            "Deleting product {ProductId}",
            id
        );

        return true;
    }
}