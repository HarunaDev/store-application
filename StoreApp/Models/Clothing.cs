namespace StoreApp.Models;

public class Clothing : Product, IDiscountable
{
    public int Size { get; set; }

    public Clothing(
        int id,
        string name,
        decimal price,
        int size,
        bool hasDiscount,
        int categoryId
    ) : base(id, name, price, hasDiscount, categoryId)
    {
        Size = size;
    }

    public string GetSizeName()
    {
        return Size switch
        {
            1 => "SM",
            2 => "MD",
            3 => "LG",
            _ => "Unknown"
        };
    }

    public decimal ApplyDiscount(decimal percentage)
    {
        return CalculateDiscount(Price, percentage);
    }
}