using Microsoft.EntityFrameworkCore;
using Supabase;
using Supabase.Storage;
using StoreApp.Data;
using StoreApp.DTOs;
using StoreApp.DTOs.Product;
using StoreApp.Models;
using StoreApp.Exceptions;
using StoreApp.Extensions;
using StoreApp.DTOs.Responses;


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

    public async Task<(IEnumerable<Product> Items, PagedResponse<Product> Meta)> GetProductsAsync(int pageNumber, int pageSize)
    {
        var query = _context.Products
            .Include(p => p.Category)
            .OrderBy(p => p.Id);
        return await query.ToPagedResponseAsync(pageNumber, pageSize);
        // return await _context.Products.Include(p => p.Category).OrderBy(p => p.Id).ToListAsync();
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        var product = await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);

        if (product is null)
        {
            throw new NotFoundException("Product not found.");
        }

        return product;
        // return await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
        // return await _context.Products.FindAsync(id);
    }

    public async Task<Product> AddProductAsync(CreateProductDto dto)
    {
        // int nextId = _products.Any()
        // ? _products.Max(p => p.Id) + 1
        // : 1;


        var category = await _categoryService.GetCategoryByIdAsync(dto.CategoryId);

        // if (category is null)
        //     throw new ValidationException("Invalid category");

        var exists = await _context.Products
    .AnyAsync(p => p.Name == dto.Name);

        if (exists)
        {
            throw new ConflictException("A product with this name already exists.");
        }

        string? imageUrl = null;
        if (dto.ImageFile != null)
        {
            // Upload to Supabase Storage
            using var stream = dto.ImageFile.OpenReadStream();
            using var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);
            var fileBytes = memoryStream.ToArray();
            var fileName = $"{Guid.NewGuid()}_{dto.ImageFile.FileName}";

            // Use Supabase client SDK to upload
            var supabase = new Supabase.Client(
                Environment.GetEnvironmentVariable("SUPABASE_URL")!,
                Environment.GetEnvironmentVariable("SUPABASE_KEY")!
            );

            try
            {
                await supabase.Storage
                .From("product-images")
                .Upload(fileBytes, fileName, new Supabase.Storage.FileOptions
                {
                    ContentType = dto.ImageFile.ContentType
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Image upload failed.");
                throw new ValidationException("Unable to upload image.");
            }
            imageUrl = supabase.Storage.From("product-images").GetPublicUrl(fileName);
        }

        var product = new Product
        {
            Name = dto.Name,
            Price = dto.Price,
            HasDiscount = dto.HasDiscount,
            CategoryId = dto.CategoryId,
            Category = category,

            Size = dto.Size,
            Warranty = dto.Warranty,
            Brand = dto.Brand,
            ImageUrl = imageUrl
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

    public async Task<Product> UpdateProductAsync(int id, UpdateProductDto dto)
    {
        var existingProduct = await GetProductByIdAsync(id);

        // if (existingProduct is null)
        // {
        //     _logger.LogWarning(
        //         "Update failed. Product {ProductId} not found",
        //         id
        //     );
        //     return null;
        // }

        var category = await _categoryService.GetCategoryByIdAsync(dto.CategoryId);
        // if (category is null)
        //     throw new Exception("Invalid category");

        var duplicate = await _context.Products.AnyAsync(p => p.Name == dto.Name && p.Id != id);

        if (duplicate)
        {
            throw new ConflictException("A product with this name already exists.");
        }

        existingProduct.Name = dto.Name;
        existingProduct.Price = dto.Price;
        existingProduct.HasDiscount = dto.HasDiscount;
        existingProduct.CategoryId = dto.CategoryId;
        existingProduct.Category = category;
        existingProduct.Size = dto.Size;
        existingProduct.Warranty = dto.Warranty;
        existingProduct.Brand = dto.Brand;

        if (dto.ImageFile != null)
        {
            existingProduct.ImageUrl = await UploadProductImageAsync(dto.ImageFile);
        }

        await _context.SaveChangesAsync();
        _logger.LogInformation(
            "Updating product {ProductId}",
            id
        );

        return existingProduct;
    }

    public async Task DeleteProductAsync(int id)
    {
        var product = await GetProductByIdAsync(id);

        // if (product is null)
        // {
        //     _logger.LogWarning(
        //         "Delete failed. Product {ProductId} not found",
        //         id
        //     );
        //     return false;
        // }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        _logger.LogInformation(
            "Deleting product {ProductId}",
            id
        );
    }

    private async Task<string> UploadProductImageAsync(IFormFile imageFile)
    {
        try
        {
            using var stream = imageFile.OpenReadStream();
            using var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);
            var fileBytes = memoryStream.ToArray();
            var fileName = $"{Guid.NewGuid()}_{imageFile.FileName}";

            var supabase = new Supabase.Client(Environment.GetEnvironmentVariable("SUPABASE_URL")!,
            Environment.GetEnvironmentVariable("SUPABASE_KEY")!);

            await supabase.Storage
            .From("product-images")
            .Upload(fileBytes, fileName, new Supabase.Storage.FileOptions
            {
                ContentType = imageFile.ContentType
            });

            return supabase.Storage.From("product-images").GetPublicUrl(fileName);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to upload image.");

            throw;
        }
    }
}