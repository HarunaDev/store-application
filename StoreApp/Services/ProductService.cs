// using WebApi.Models;
using StoreApp.Models;

namespace StoreApp.Services;

public class ProductService
{
    public List<Product> GetProducts()
    {
        return new List<Product>
        {
            new Clothing(
                "Samo Vintage Shirt",
                49.99m,
                2,
                true
            ),

            new Electronics(
                "Television",
                334.55m,
                true,
                "Philips",
                true
            )
        };
    }
}