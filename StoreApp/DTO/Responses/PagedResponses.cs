using System.Text.Json.Serialization;
using StoreApp.DTOs.Product;
// using StoreApp.DTOs.Category;

namespace StoreApp.DTOs.Responses
{
    // Generic metadata
    public class PagedResponse<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalRecords / PageSize);
    }

    // Product-specific wrapper
    public class ProductPagedResponse : PagedResponse<ProductDto>
    {
        [JsonPropertyName("products")]
        public IEnumerable<ProductDto> Products { get; set; } = Enumerable.Empty<ProductDto>();
    }

    // Category-specific wrapper
    // public class CategoryPagedResponse : PagedResponse<CategoryDto>
    // {
    //     [JsonPropertyName("categories")]
    //     public IEnumerable<CategoryDto> Categories { get; set; } = Enumerable.Empty<CategoryDto>();
    // }
}
