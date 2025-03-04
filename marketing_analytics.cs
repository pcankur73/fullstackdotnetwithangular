using System;
using System.Collections.Generic;
using System.IO;

// Case Study: Customer Data Analytics for a Marketing Campaign
// This C# program manages customer data, performs analytics, and ensures memory efficiency.

class Customer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }
    public double PurchaseAmount { get; set; }
}

class MarketingAnalytics
{
    private List<Customer> customers = new List<Customer>();
    private string filePath = "customers.txt";

    // Load data from file
    public void LoadData()
    {
        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                string[] data = line.Split(',');
                customers.Add(new Customer
                {
                    Id = int.Parse(data[0]),
                    Name = data[1],
                    Age = int.Parse(data[2]),
                    Email = data[3],
                    PurchaseAmount = double.Parse(data[4])
                });
            }
        }
    }

    // Save data to file
    public void SaveData()
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (var customer in customers)
            {
                writer.WriteLine($"{customer.Id},{customer.Name},{customer.Age},{customer.Email},{customer.PurchaseAmount}");
            }
        }
    }

    // Add a new customer
    public void AddCustomer(Customer customer)
    {
        customers.Add(customer);
    }

    // Perform analytics: Calculate average purchase amount
    public double CalculateAveragePurchase()
    {
        double total = 0;
        foreach (var customer in customers)
        {
            total += customer.PurchaseAmount;
        }
        return customers.Count > 0 ? total / customers.Count : 0;
    }

    // Memory Management: Explicit Garbage Collection
    public void Cleanup()
    {
        customers.Clear();
        GC.Collect(); // Force garbage collection
        GC.WaitForPendingFinalizers();
    }
}

class Program
{
    static void Main()
    {
        MarketingAnalytics analytics = new MarketingAnalytics();
        analytics.LoadData();

        analytics.AddCustomer(new Customer { Id = 1, Name = "Alice", Age = 30, Email = "alice@example.com", PurchaseAmount = 250.50 });
        analytics.AddCustomer(new Customer { Id = 2, Name = "Bob", Age = 40, Email = "bob@example.com", PurchaseAmount = 150.75 });

        analytics.SaveData();

        Console.WriteLine("Average Purchase Amount: " + analytics.CalculateAveragePurchase());

        analytics.Cleanup();
    }
}