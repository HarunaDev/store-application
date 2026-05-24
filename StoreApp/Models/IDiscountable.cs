namespace StoreApp.Models;

public interface IDiscountable
{
    decimal ApplyDiscount(decimal percentage);
}