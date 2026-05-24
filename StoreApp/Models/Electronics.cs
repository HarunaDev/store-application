namespace StoreApp.Models;

public class Electronics : Product, IDiscountable
{
    public bool Warranty { get; set; }

    public string Brand { get; set; } = "";

    public Electronics(
        string name,
        decimal price,
        bool warranty,
        string brand,
        bool hasDiscount
    ) : base(name, price, hasDiscount)
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