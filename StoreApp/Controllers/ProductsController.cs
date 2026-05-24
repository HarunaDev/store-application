using Microsoft.AspNetCore.Mvc;
using StoreApp.Models;
using StoreApp.Services;

namespace StoreApp.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly ProductService _productService;

    public ProductsController(ProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public IActionResult GetProducts()
    {
        return Ok(_productService.GetProducts());
    }

    [HttpGet("{id}")]
    public IActionResult GetProduct(int id)
    {
        var product = _productService.GetProductById(id);

        if (product is null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    [HttpPost]
    public IActionResult AddProduct(Product product)
    {
        var createdProduct = _productService.AddProduct(product);

        return CreatedAtAction(
            nameof(GetProduct),
            new { id = createdProduct.Id },
            createdProduct
        );
    }

    [HttpPut("{id}")]
    public IActionResult UpdateProduct(
        int id,
        Product updatedProduct
    )
    {
        var updated = _productService.UpdateProduct(
            id,
            updatedProduct
        );

        if (!updated)
        {
            return NotFound();
        }

        return Ok(updatedProduct);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteProduct(int id)
    {
        var deleted = _productService.DeleteProduct(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}