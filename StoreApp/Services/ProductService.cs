using StoreApp.Models;

namespace StoreApp.Services;

public class ProductService
{
    private readonly List<Product> _products;

    public ProductService()
    {
        _products = new List<Product>
        {
            new Clothing(
                1,
                "Samo Vintage Shirt",
                49.99m,
                2,
                true
            ),

            new Electronics(
                2,
                "Television",
                334.55m,
                true,
                "Philips",
                true
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

    public Product AddProduct(Product product)
    {
        product.Id = _products.Max(p => p.Id) + 1;

        _products.Add(product);

        return product;
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