using System;
using System.Collections.Generic;
using System.IO;

class Order
{
    private List<Product> products;
    private Customer customer;

    public Order(Customer customer)
    {
        this.customer = customer;
        products = new List<Product>();
    }

    public void AddProduct(Product product)
    {
        products.Add(product);
    }

    public decimal CalculateTotalCost()
    {
        decimal totalCost = 0;
        foreach (Product product in products)
        {
            totalCost += product.CalculateTotalPrice();
        }

        // Add one-time shipping cost based on customer location
        totalCost += customer.IsInUSA() ? 5 : 35;

        return totalCost;
    }

    public string GetPackingLabel()
    {
        string packingLabel = "Packing Label:\n";
        foreach (Product product in products)
        {
            packingLabel += $"{product.Name} (ID: {product.ProductId})\n";
        }
        return packingLabel;
    }

    public string GetShippingLabel()
    {
        return $"Shipping Label:\n{customer.Name}\n{customer.Address.ToString()}";
    }
}

class Product
{
    private string name;
    private string productId;
    private decimal price;
    private int quantity;

    public Product(string name, string productId, decimal price, int quantity)
    {
        this.name = name;
        this.productId = productId;
        this.price = price;
        this.quantity = quantity;
    }

    public decimal CalculateTotalPrice()
    {
        return price * quantity;
    }

    public string Name => name;
    public string ProductId => productId;
}

class Customer
{
    private string name;
    private Address address;

    public Customer(string name, Address address)
    {
        this.name = name;
        this.address = address;
    }

    public bool IsInUSA()
    {
        return address.IsInUSA();
    }

    public string Name => name;
    public Address Address => address;
}

class Address
{
    private string street;
    private string city;
    private string stateProvince;
    private string country;

    public Address(string street, string city, string stateProvince, string country)
    {
        this.street = street;
        this.city = city;
        this.stateProvince = stateProvince;
        this.country = country;
    }

    public bool IsInUSA()
    {
        return string.Equals(country, "USA", StringComparison.OrdinalIgnoreCase);
    }

    public override string ToString()
    {
        return $"{street}, {city}, {stateProvince}, {country}";
    }
}

class Program
{
    static void Main()
    {
        // Create addresses
        Address usaAddress = new Address("11287 Bobcat Ln", "Sunnyvale", "CA", "USA");
        Address nonUsaAddress = new Address("7th Ave SW", "Townsville", "ON", "Canada");

        // Create customers
        Customer usaCustomer = new Customer("Stephanie Dacullo", usaAddress);
        Customer nonUsaCustomer = new Customer("Steph Laruga", nonUsaAddress);

        // Create products
        Product laptop = new Product("Laptop", "LPT098", 210, 2);
        Product headphones = new Product("Headphones", "HPH141", 53, 3);

        // Create orders
        Order order1 = new Order(usaCustomer);
        order1.AddProduct(laptop);
        order1.AddProduct(headphones);

        Order order2 = new Order(nonUsaCustomer);
        order2.AddProduct(new Product("Smartphone", "SPH367", 658, 1));
        order2.AddProduct(new Product("Tablet", "TAB573", 326, 2));

        // Display results
        Console.WriteLine("Order 1:");
        Console.WriteLine(order1.GetPackingLabel());
        Console.WriteLine(order1.GetShippingLabel());
        Console.WriteLine($"Total Cost: ${order1.CalculateTotalCost()}\n");

        Console.WriteLine("Order 2:");
        Console.WriteLine(order2.GetPackingLabel());
        Console.WriteLine(order2.GetShippingLabel());
        Console.WriteLine($"Total Cost: ${order2.CalculateTotalCost()}");
    }
}
