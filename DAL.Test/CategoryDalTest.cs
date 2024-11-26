using NUnit.Framework;
using DAL.Concrete;
using DTO;
using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
using Microsoft.Data.SqlClient;

[TestFixture]
public class CategoryDalTests
{
    private string _connectionString = "Data Source=HOME-PC;Initial Catalog=ItemReceiver;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
    private CategoryDal _categoryDal;

    [SetUp]
    public void SetUp()
    {
        
        _categoryDal = new CategoryDal(_connectionString);

        
        //ClearCategoriesTable();
    }

    
    [Test]
    public void AddCategory_ShouldAddCategorySuccessfully()
    {
        
        var newCategory = new CategoryDto { Name = "Test Category" };

        
        _categoryDal.AddCategory(newCategory);

        
        var addedCategory = _categoryDal.GetCategoryById(newCategory.Id);
        Assert.IsNotNull(addedCategory);
        Assert.AreEqual(newCategory.Name, addedCategory.Name);
    }

    
    [Test]
    public void GetCategories_ShouldReturnCategories()
    {
        
        _categoryDal.AddCategory(new CategoryDto { Name = "Test Category 1" });
        _categoryDal.AddCategory(new CategoryDto { Name = "Test Category 2" });

        
        var categories = _categoryDal.GetCategories();

        
        Assert.IsNotNull(categories);
        Assert.AreEqual(2, categories.Count);
    }

    // категорії за ID
    [Test]
    public void GetCategoryById_ShouldReturnCategory()
    {
        
        var category = new CategoryDto { Name = "Test Category" };
        _categoryDal.AddCategory(category);

        
        var fetchedCategory = _categoryDal.GetCategoryById(category.Id);

        
        Assert.IsNotNull(fetchedCategory);
        Assert.AreEqual(category.Name, fetchedCategory.Name);
    }

    
    [Test]
    public void UpdateCategory_ShouldUpdateCategorySuccessfully()
    {
        
        var category = new CategoryDto { Name = "Old Category" };
        _categoryDal.AddCategory(category);

        category.Name = "Updated Category";

        
        _categoryDal.UpdateCategory(category);

        
        var updatedCategory = _categoryDal.GetCategoryById(category.Id);
        Assert.AreEqual("Updated Category", updatedCategory.Name);
    }

    
    [Test]
    public void DeleteCategory_ShouldDeleteCategorySuccessfully()
    {
        
        var category = new CategoryDto { Name = "Category to Delete" };
        _categoryDal.AddCategory(category);

        
        _categoryDal.DeleteCategory(category.Id);

        
        var deletedCategory = _categoryDal.GetCategoryById(category.Id);
        Assert.IsNull(deletedCategory);
    }

    // Очистка таблиці Categories після кожного тесту
    private void ClearCategoriesTable()
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            var query = "DELETE FROM Categories";
            using (var command = new SqlCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }

    [TearDown]
    public void TearDown()
    {
        // Очистка даних після кожного тесту
        ClearCategoriesTable();
    }
}
