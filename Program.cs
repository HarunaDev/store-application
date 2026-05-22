using System.Reflection;

namespace CProduction
{
    // interface for discountable products
    public interface IDiscountable
    {
        decimal ApplyDiscount(decimal percentage);
    }


    // base product class
    class Product
    {
        // private field
        private decimal _price;

        // public property
        public string Name { get; set; }

        // public property with additional logic in setter
        public decimal Price
        {
            get { return _price; }
            set
            {
                // prevents setting the price to negative
                if (value >= 0) _price = value;
            }
        }

        // constrctor
        public Product(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        // virtual method
        public virtual void DisplayProductDetails()
        {
            Console.WriteLine($"Product: {Name}, Price: {Price:C}");
        }

        // static method to calculate discount
        public static decimal CalculateDiscount(decimal price, decimal discountPercentage)
        {
            return price - (price * discountPercentage / 100);
        }
    }
}