using System;
using System.Collections.Generic;
using System.IO;

// Supply Chain Management System
namespace SCM_CaseStudy
{
    class Program
    {
        static void Main(string[] args)
        {
            Inventory inventory = new Inventory();
            inventory.LoadInventoryFromFile("inventory.txt");
            inventory.AddProduct(new Product(101, "Laptop", 50));
            inventory.AddProduct(new Product(102, "Mobile", 100));
            inventory.DisplayInventory();
            inventory.SaveInventoryToFile("inventory.txt");

            GC.Collect(); // Explicit garbage collection (not recommended but shown for learning)
            Console.WriteLine("Garbage Collection triggered!");
        }
    }

    // Product Class
    class Product
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public int Quantity { get; set; }

        public Product(int id, string name, int quantity)
        {
            Id = id;
            Name = name;
            Quantity = quantity;
        }

        public override string ToString()
        {
            return $"{Id},{Name},{Quantity}";
        }
    }

    // Inventory Management
    class Inventory
    {
        private List<Product> products = new List<Product>();

        // Add product to inventory
        public void AddProduct(Product product)
        {
            products.Add(product);
        }

        // Display inventory
        public void DisplayInventory()
        {
            Console.WriteLine("\nCurrent Inventory:");
            foreach (var product in products)
            {
                Console.WriteLine($"ID: {product.Id}, Name: {product.Name}, Quantity: {product.Quantity}");
            }
        }

        // Save inventory to file
        public void SaveInventoryToFile(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var product in products)
                {
                    writer.WriteLine(product.ToString());
                }
            }
        }

        // Load inventory from file
        public void LoadInventoryFromFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split(',');
                        if (parts.Length == 3)
                        {
                            int id = int.Parse(parts[0]);
                            string name = parts[1];
                            int quantity = int.Parse(parts[2]);
                            products.Add(new Product(id, name, quantity));
                        }
                    }
                }
            }
        }
    }
}
