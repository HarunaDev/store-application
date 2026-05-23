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
        public bool HasDiscount { get; set; }

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
        public Product(string name, decimal price, bool hasDiscount)
        {
            Name = name;
            Price = price;
            HasDiscount = hasDiscount;
        }

        // virtual method
        public virtual void DisplayProductDetails()
        {
            Console.WriteLine($"Product: {Name}, Price: {Price:C}");
            if (HasDiscount && this is IDiscountable discountable)
            {
                decimal discountedPrice = discountable.ApplyDiscount(5);

                Console.WriteLine($"Discounted Price: {discountedPrice:C}");
            }
        }

        // static method
        public static decimal CalculateDiscount(decimal price, decimal discountPercentage)
        {
            return price - (price * (discountPercentage / 100));
        }
    }

    // subclass
    class Clothing : Product, IDiscountable
    {
        public int Size { get; set; }

        public Clothing(string name, decimal price, int size, bool hasDiscount)
            : base(name, price, hasDiscount)
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
        public string Brand { set; get; }

        public Electronics(string name, decimal price, bool warranty, string brand, bool hasDiscount)
            : base(name, price, hasDiscount)
        {
            Warranty = warranty;
            Brand = brand;
        }

        public string GetWarrantyMessage()
        {
            if (Warranty)
            {
                return "Warranty for 2 years available";
            }

            return "Can be replaced within 24 hours if returned in good condition";
        }
        public override void DisplayProductDetails()
        {
            base.DisplayProductDetails();
            Console.WriteLine($"Brand: {Brand}");
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
            List<Product> products = new List<Product>();
            // List<Electronics> items = new List<Electronics>();

            // Clothing
            products.Add(new Clothing("Samo Vintage Shirt", 49.99m, 2, true));
            products.Add(new Clothing("Short Pants", 9.99m, 1, false));
            products.Add(new Clothing("Traditional Wears", 82.99m, 3, true));

            // Electronics
            products.Add(new Electronics("Kettle", 34.55m, true, "Philips", false));
            products.Add(new Electronics("Kettle", 34.55m, false, "LG", true));
            products.Add(new Electronics("Washing Machine", 134.55m, true, "LG", false));
            products.Add(new Electronics("Television", 334.55m, true, "Philips", true));

            // foreach loop
            foreach (Product product in products)
            {
                product.DisplayProductDetails();
                Console.WriteLine("----------------");

            }

            decimal discountedPrice = ((IDiscountable)products[0]).ApplyDiscount(10);

            Console.WriteLine($"Discounted price: {discountedPrice:C}");

            // Console.WriteLine(Product.CalculateDiscount(50m, 10m));
        }
    }
}