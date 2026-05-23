using System;

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

        // public property with validation
        public decimal Price
        {
            get { return _price; }
            set
            {
                if (value >= 0)
                    _price = value;
            }
        }

        // constructor
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

        // static method
        public static decimal CalculateDiscount(decimal price, decimal discountPercentage)
        {
            return price - (price * discountPercentage / 100);
        }
    }

    // subclass
    class Clothing : Product, IDiscountable
    {
        public int Size { get; set; }

        public Clothing(string name, decimal price, int size)
            : base(name, price)
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
                _ => "Unknown Size"
            };
        }

        public override void DisplayProductDetails()
        {
            base.DisplayProductDetails();
            Console.WriteLine($"Size: {GetSizeName()}");
        }

        public decimal ApplyDiscount(decimal percentage)
        {
            return CalculateDiscount(Price, percentage);
        }
    }

    // electronics
    class Electronics : Product, IDiscountable
    {
        public bool Warranty { set; get; }

        public Electronics(string name, decimal price, bool warranty)
            : base(name, price)
        {
            Warranty = warranty;
        }

        public string GetWarrantyMessage()
        {
            return Warranty switch
            {
                true => "Warranty for 2 years Available",
                false => "Product can be replaced if returned in good condition within 24hrs",
                _ => "Unknown"
            };
        }
        public override void DisplayProductDetails()
        {
            base.DisplayProductDetails();
            Console.WriteLine($"Warranty: {GetWarrantyMessage()}");
        }

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

            catalog.Add(new Clothing("Samo Vintage Shirt", 49.99m, 2));
            catalog.Add(new Clothing("Short Pants", 9.99m, 1));
            catalog.Add(new Clothing("Traditional Wears", 82.99m, 3));

            // for loop
            for (int i = 0; i < catalog.Count; i++)
            {
                catalog[i].DisplayProductDetails();
            }

            // foreach loop
            foreach (Clothing item in catalog)
            {
                item.DisplayProductDetails();
            }

            decimal discountedPrice = catalog[0].ApplyDiscount(10);

            Console.WriteLine($"Discounted price: {discountedPrice:C}");

            Console.WriteLine(Product.CalculateDiscount(50m, 10m));
        }
    }
}