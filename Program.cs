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

    class Program
    {
        static void Main()
        {
            List<Clothing> catalog = new List<Clothing>();
            // Creating a clothing objects
            catalog.Add(new Clothing("Samo vintage shirt", 49.99m, 2));
            catalog.Add(new Clothing("Short Pants", 9.99m, 1));
            catalog.Add(new Clothing("Traditional Wears", 82.99m, 2));

            // display product details
            for (int i = 0; 1 < catalog.Count; i++)
            {
                catalog(i).DisplayProductDetails();
            }

            foreach (Clothing item in catalog)
            {
                item.DisplayProductDetails();
            }

            // Apply discount to the clothing product
            decimal discountedPrice = catalog[0].ApplyDiscount(10);
            Console.WriteLine($"Shorts price after discount: {discountedPrice:C}");
            Console.WriteLine(Product.CalculateDiscount(29, 50m, 0.1m));
        }
    }
}