using StoreApp.DTOs;
using StoreApp.Models;

namespace StoreApp.Services;

public class ProductService
{
    private readonly List<Product> _products;

    private readonly CategoryService _categoryService;

    public ProductService(CategoryService categoryService)
    {
        _categoryService = categoryService;
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
        Product product;

        if (dto.ProductType.ToLower() == "clothing")
        {
            product = new Clothing(
                _products.Max(p => p.Id) + 1,
                dto.Name,
                dto.Price,
                dto.Size ?? 1,
                dto.HasDiscount,
                dto.CategoryId
            );
        }
        else if (dto.ProductType.ToLower() == "electronics")
        {
            product = new Electronics(
                _products.Max(p => p.Id) + 1,
                dto.Name,
                dto.Price,
                dto.Warranty ?? false,
                dto.Brand ?? "",
                dto.HasDiscount,
                dto.CategoryId
            );
        }
        else
        {
            throw new Exception("Invalid product type");
        }

        product.Category = _categoryService.GetCategoryById(dto.CategoryId);

        _products.Add(product);

        return product;

        // product.Id = _products.Max(p => p.Id) + 1;

        // _products.Add(product);

        // return product;
    }

    public bool UpdateProduct(int id, Product updatedProduct)
    {
        var existingProduct = GetProductById(id);

        if (existingProduct is null)
        {
            return false;
        }

        existingProduct.Name = updatedProduct.Name;
        existingProduct.Price = updatedProduct.Price;
        existingProduct.HasDiscount = updatedProduct.HasDiscount;

        return true;
    }

    public bool DeleteProduct(int id)
    {
        var product = GetProductById(id);

        if (product is null)
        {
            return false;
        }

        _products.Remove(product);

        return true;
    }
}