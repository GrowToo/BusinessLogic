using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using DAL.Concrete;
using DTO;
using System.Linq;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Зчитуємо налаштування з appsettings.json
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())  // Задаємо поточну директорію як базову для конфігурації
                .AddJsonFile(@"D:\\ПЗ\\1\\BusinessLogic\\ConsoleApp\\appsettings.json")  // Додаємо файл конфігурації
                .Build();

            // Отримуємо рядок підключення з конфігурації
            string connectionString = configuration.GetConnectionString("ItemReceiver");

            // Створюємо екземпляри DAL класів з передачею рядка підключення
            var productDal = new ProductDal(connectionString);
            var categoryDal = new CategoryDal(connectionString);

            Console.WriteLine("Welcome to the Product Management System!");

            // Меню
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Add Product");
                Console.WriteLine("2. View All Products");
                Console.WriteLine("3. Update Product");
                Console.WriteLine("4. Delete Product");
                Console.WriteLine("5. Search Product");
                Console.WriteLine("6. Exit");
                Console.Write("Choose an option: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddProduct(productDal);
                        break;

                    case "2":
                        ViewAllProducts(productDal);
                        break;

                    case "3":
                        UpdateProduct(productDal);
                        break;

                    case "4":
                        DeleteProduct(productDal);
                        break;

                    case "5":
                        SearchProduct(productDal);
                        break;

                    case "6":
                        Console.WriteLine("Exiting the application...");
                        return;

                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        break;
                }
            }
        }

        // Додавання нового продукту
        static void AddProduct(ProductDal productDal)
        {
            Console.Clear();
            Console.WriteLine("Enter Product Name:");
            var name = Console.ReadLine();
            Console.WriteLine("Enter Category ID:");
            var categoryId = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter Quantity:");
            var quantity = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter Price:");
            var price = decimal.Parse(Console.ReadLine());

            var newProduct = new ProductDto
            {
                Name = name,
                CategoryId = categoryId,
                Quantity = quantity,
                Price = price,
                CreatedDate = DateTime.Now
            };

            productDal.AddProduct(newProduct);
            Console.WriteLine("Product added successfully!");
            Console.ReadLine();
        }

        // Перегляд всіх продуктів
        static void ViewAllProducts(ProductDal productDal)
        {
            Console.Clear();
            var products = productDal.GetProducts();

            if (products.Any())
            {
                Console.WriteLine("List of All Products:");
                foreach (var product in products)
                {
                    Console.WriteLine($"ID: {product.Id}, Name: {product.Name}, Price: {product.Price}, Quantity: {product.Quantity}, Category ID: {product.CategoryId}, Created Date: {product.CreatedDate}");
                }
            }
            else
            {
                Console.WriteLine("No products found.");
            }

            Console.ReadLine();
        }

        // Оновлення інформації про продукт
        static void UpdateProduct(ProductDal productDal)
        {
            Console.Clear();
            Console.WriteLine("Enter Product ID to Update:");
            var updateId = int.Parse(Console.ReadLine());

            var productToUpdate = productDal.GetProductById(updateId);
            if (productToUpdate != null)
            {
                Console.WriteLine($"Current Name: {productToUpdate.Name}");
                Console.WriteLine("Enter new Name:");
                productToUpdate.Name = Console.ReadLine();

                Console.WriteLine($"Current Quantity: {productToUpdate.Quantity}");
                Console.WriteLine("Enter new Quantity:");
                productToUpdate.Quantity = int.Parse(Console.ReadLine());

                Console.WriteLine($"Current Price: {productToUpdate.Price}");
                Console.WriteLine("Enter new Price:");
                productToUpdate.Price = decimal.Parse(Console.ReadLine());

                productDal.UpdateProduct(productToUpdate);
                Console.WriteLine("Product updated successfully!");
            }
            else
            {
                Console.WriteLine("Product not found.");
            }

            Console.ReadLine();
        }

        // Видалення продукту
        static void DeleteProduct(ProductDal productDal)
        {
            Console.Clear();
            Console.WriteLine("Enter Product ID to Delete:");
            var deleteId = int.Parse(Console.ReadLine());
            productDal.DeleteProduct(deleteId);
            Console.WriteLine("Product deleted successfully!");
            Console.ReadLine();
        }

        // Пошук продукту за назвою
        static void SearchProduct(ProductDal productDal)
        {
            Console.Clear();
            Console.WriteLine("Enter Product Name to Search:");
            var searchName = Console.ReadLine();

            var products = productDal.GetProducts().Where(p => p.Name.Contains(searchName, StringComparison.OrdinalIgnoreCase)).ToList();

            if (products.Any())
            {
                Console.WriteLine("Search Results:");
                foreach (var product in products)
                {
                    Console.WriteLine($"ID: {product.Id}, Name: {product.Name}, Price: {product.Price}, Quantity: {product.Quantity}, Category ID: {product.CategoryId}, Created Date: {product.CreatedDate}");
                }
            }
            else
            {
                Console.WriteLine("No products found with that name.");
            }

            Console.ReadLine();
        }
    }
}
