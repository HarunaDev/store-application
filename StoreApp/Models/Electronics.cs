namespace StoreApp.Models;

public class Electronics : Product, IDiscountable
{
    public bool Warranty { get; set; }

    public string Brand { get; set; } = "";

    public Electronics(
        int id,
        string name,
        decimal price,
        bool warranty,
        string brand,
        bool hasDiscount,
        int categoryId
    ) : base(id, name, price, hasDiscount, categoryId)
    {
        Warranty = warranty;
        Brand = brand;
    }

    public string GetWarrantyMessage()
    {
        if (Warranty)
        {
            return "Warranty available";
        }

        return "No warranty";
    }

    public decimal ApplyDiscount(decimal percentage)
    {
        return CalculateDiscount(Price, percentage);
    }
}