using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using DAL.Interface;
using DTO;

namespace DAL.Concrete
{
    public class CategoryDal : ICategoryDal
    {
        private readonly string _connectionString;

        public CategoryDal(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<CategoryDto> GetCategories()
        {
            List<CategoryDto> categories = new List<CategoryDto>();

            using (SqlConnection connection = new SqlConnection(DBConfiguration.ConnectionString))
            {
                connection.Open();
                string query = "SELECT Id, Name FROM Categories";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        categories.Add(new CategoryDto
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString()
                        });
                    }
                }
            }
            return categories;
        }

        public void AddCategory(CategoryDto categoryDto)
        {
            using (SqlConnection connection = new SqlConnection(DBConfiguration.ConnectionString))
            {
                connection.Open();
                string query = "INSERT INTO Categories (Name) VALUES (@Name)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", categoryDto.Name);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Отримання категорії за ID
        public CategoryDto GetCategoryById(int id)
        {
            using (SqlConnection connection = new SqlConnection(DBConfiguration.ConnectionString))
            {
                connection.Open();
                string query = "SELECT Id, Name FROM Categories WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        return new CategoryDto
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString()
                        };
                    }
                }
            }
            return null;
        }

        // Оновлення категорії
        public void UpdateCategory(CategoryDto categoryDto)
        {
            using (SqlConnection connection = new SqlConnection(DBConfiguration.ConnectionString))
            {
                connection.Open();
                string query = "UPDATE Categories SET Name = @Name WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", categoryDto.Name);
                    command.Parameters.AddWithValue("@Id", categoryDto.Id);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Видалення категорії
        public void DeleteCategory(int id)
        {
            using (SqlConnection connection = new SqlConnection(DBConfiguration.ConnectionString))
            {
                connection.Open();
                string query = "DELETE FROM Categories WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
