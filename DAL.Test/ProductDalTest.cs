using NUnit.Framework;
using DAL.Concrete;
using DTO;
using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;

[TestFixture]
public class ProductDalTests
{
    private string _connectionString;
    private ProductDal _productDal;

    [SetUp]
    public void SetUp()
    {
        
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("C:\\Users\\User\\source\\repos\\Shop\\config.json")
            .Build();

        // Отримання рядка підключення з конфігурації
        _connectionString = configuration.GetConnectionString("ItemReceiver") ?? throw new InvalidOperationException("Connection string is missing.");

        
        _productDal = new ProductDal(_connectionString);
    }

    [Test]
    public void AddProduct_ShouldAddProductToDatabase()
    {
        // Arrange
        var product = new ProductDto
        {
            Name = "Test Product",
            CategoryId = 1, // Assuming category with Id 1 exists
            Quantity = 10,
            Price = 100.5m,
            CreatedDate = DateTime.Now
        };

        // Act
        _productDal.AddProduct(product);

        // Assert
        var addedProduct = _productDal.GetProductById(product.Id);
        Assert.IsNotNull(addedProduct);
        Assert.AreEqual(product.Name, addedProduct.Name);
        Assert.AreEqual(product.CategoryId, addedProduct.CategoryId);
        Assert.AreEqual(product.Quantity, addedProduct.Quantity);
        Assert.AreEqual(product.Price, addedProduct.Price);
    }

    [Test]
    public void UpdateProduct_ShouldUpdateProductInDatabase()
    {
        // Arrange
        var product = new ProductDto
        {
            Name = "Old Product Name",
            CategoryId = 1,
            Quantity = 10,
            Price = 50.0m,
            CreatedDate = DateTime.Now
        };

        // Add product to database
        _productDal.AddProduct(product);

        // Update product details
        product.Name = "Updated Product Name";
        product.Quantity = 20;
        product.Price = 60.0m;

        // Act
        _productDal.UpdateProduct(product);

        // Assert
        var updatedProduct = _productDal.GetProductById(product.Id);
        Assert.IsNotNull(updatedProduct);
        Assert.AreEqual("Updated Product Name", updatedProduct.Name);
        Assert.AreEqual(20, updatedProduct.Quantity);
        Assert.AreEqual(60.0m, updatedProduct.Price);
    }

    [Test]
    public void DeleteProduct_ShouldRemoveProductFromDatabase()
    {
        // Arrange
        var product = new ProductDto
        {
            Name = "Product to Delete",
            CategoryId = 1,
            Quantity = 10,
            Price = 100.0m,
            CreatedDate = DateTime.Now
        };

        // Add product to database
        _productDal.AddProduct(product);

        // Act
        _productDal.DeleteProduct(product.Id);

        // Assert
        var deletedProduct = _productDal.GetProductById(product.Id);
        Assert.IsNull(deletedProduct);
    }

    [Test]
    public void GetProductById_ShouldReturnProductFromDatabase()
    {
        // Arrange
        var product = new ProductDto
        {
            Name = "Product to Retrieve",
            CategoryId = 1,
            Quantity = 10,
            Price = 100.0m,
            CreatedDate = DateTime.Now
        };

        // Add product to database
        _productDal.AddProduct(product);

        // Act
        var retrievedProduct = _productDal.GetProductById(product.Id);

        // Assert
        Assert.IsNotNull(retrievedProduct);
        Assert.AreEqual(product.Name, retrievedProduct.Name);
        Assert.AreEqual(product.CategoryId, retrievedProduct.CategoryId);
        Assert.AreEqual(product.Quantity, retrievedProduct.Quantity);
        Assert.AreEqual(product.Price, retrievedProduct.Price);
    }

    [Test]
    public void GetProducts_ShouldReturnAllProductsFromDatabase()
    {
        // Arrange
        var product1 = new ProductDto
        {
            Name = "Product 1",
            CategoryId = 1,
            Quantity = 10,
            Price = 100.0m,
            CreatedDate = DateTime.Now
        };
        var product2 = new ProductDto
        {
            Name = "Product 2",
            CategoryId = 1,
            Quantity = 5,
            Price = 50.0m,
            CreatedDate = DateTime.Now
        };

        // Add products to database
        _productDal.AddProduct(product1);
        _productDal.AddProduct(product2);

        // Act
        var products = _productDal.GetProducts();

        // Assert
        Assert.IsNotNull(products);
        Assert.AreEqual(2, products.Count());
        Assert.Contains(product1, products.ToList());
        Assert.Contains(product2, products.ToList());
    }
}
