using System.ComponentModel;

namespace StoreApp.Models;

public class Product
{
    private decimal _price;

    public int Id { get; set; }

    public string Name { get; set; } = "";

    public bool HasDiscount { get; set; }

    public int CategoryId { get; set; }

    public Category? Category { get; set; }

    public decimal Price
    {
        get => _price;
        set
        {
            if (value >= 0)
                _price = value;
        }
    }

    public Product(
        int id,
        string name,
        decimal price,
        bool hasDiscount,
        int categoryId
    )
    {
        Id = id;
        Name = name;
        Price = price;
        HasDiscount = hasDiscount;
        CategoryId = categoryId;
    }

    public virtual decimal GetFinalPrice()
    {
        if (HasDiscount && this is IDiscountable discountable)
        {
            return discountable.ApplyDiscount(5);
        }

        return Price;
    }

    public static decimal CalculateDiscount(
        decimal price,
        decimal discountPercentage
    )
    {
        return price - (price * (discountPercentage / 100m));
    }
}