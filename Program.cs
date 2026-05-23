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

    // sub class: clothing (Discountable)
    class Clothing : Product, IDiscountable
    {
        // property to store the size as an integer
        public int Size { get; set; }

        // constructor
        public Clothing(string name, decimal price, int size) : base(name, price)
        {
            Size = size;
        }

        // method to convert size from int to a size name
        public string GetSizeName()
        {
            return Size switch
            {
                1 => "SM",
                2 => "MD",
                3 => "LG",
                _ => "Unknown Size"
            };
        }

        // Override method to include size details
        public override void DisplayProductDetails()
        {
            base.DisplayProductDetails();
            Console.WriteLine($"Size: {GetSizeName()}");
        }

        // Implementation of IDiscountable interface
        public decimal ApplyDiscount(decimal percentage)
        {
            return CalculateDiscount(Price, percentage);
        }
    }
}