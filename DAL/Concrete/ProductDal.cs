using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using DTO;

namespace DAL.Concrete
{
    public class ProductDal
    {
        private readonly string _connectionString;

        public ProductDal(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddProduct(ProductDto product)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"INSERT INTO Products (Name, CategoryId, Quantity, Price, CreatedDate)
                              VALUES (@Name, @CategoryId, @Quantity, @Price, @CreatedDate)";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", product.Name);
                    command.Parameters.AddWithValue("@CategoryId", product.CategoryId);
                    command.Parameters.AddWithValue("@Quantity", product.Quantity);
                    command.Parameters.AddWithValue("@Price", product.Price);
                    command.Parameters.AddWithValue("@CreatedDate", product.CreatedDate);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateProduct(ProductDto product)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"UPDATE Products 
                              SET Name = @Name, CategoryId = @CategoryId, Quantity = @Quantity, Price = @Price
                              WHERE Id = @Id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", product.Id);
                    command.Parameters.AddWithValue("@Name", product.Name);
                    command.Parameters.AddWithValue("@CategoryId", product.CategoryId);
                    command.Parameters.AddWithValue("@Quantity", product.Quantity);
                    command.Parameters.AddWithValue("@Price", product.Price);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteProduct(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "DELETE FROM Products WHERE Id = @Id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public ProductDto GetProductById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM Products WHERE Id = @Id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new ProductDto
                            {
                                Id = (int)reader["Id"],
                                Name = (string)reader["Name"],
                                CategoryId = (int)reader["CategoryId"],
                                Quantity = (int)reader["Quantity"],
                                Price = (decimal)reader["Price"],
                                CreatedDate = (DateTime)reader["CreatedDate"]
                            };
                        }
                    }
                }
            }

            return null;
        }

        public IEnumerable<ProductDto> GetProducts()
        {
            var products = new List<ProductDto>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM Products";

                using (var command = new SqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        products.Add(new ProductDto
                        {
                            Id = (int)reader["Id"],
                            Name = (string)reader["Name"],
                            CategoryId = (int)reader["CategoryId"],
                            Quantity = (int)reader["Quantity"],
                            Price = (decimal)reader["Price"],
                            CreatedDate = (DateTime)reader["CreatedDate"]
                        });
                    }
                }
            }

            return products;
        }
    }
}
