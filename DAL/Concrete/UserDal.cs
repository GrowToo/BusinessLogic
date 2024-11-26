using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using DAL.Interface;
using DTO;

namespace DAL.Concrete
{
    public class UserDal : IUserDal
    {
        private readonly string _connectionString;

        public UserDal(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<UserDto> GetUsers()
        {
            List<UserDto> users = new List<UserDto>();

            using (SqlConnection connection = new SqlConnection(DBConfiguration.ConnectionString))
            {
                connection.Open();
                string query = "SELECT Id, Username, Role FROM Users";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        users.Add(new UserDto
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Username = reader["Username"].ToString(),
                            Role = reader["Role"].ToString()
                        });
                    }
                }
            }
            return users;
        }

        public void Add(UserDto userDto)  // Реалізація методу Add
        {
            using (SqlConnection connection = new SqlConnection(DBConfiguration.ConnectionString))
            {
                connection.Open();
                string query = "INSERT INTO Users (Username, Password, Role) VALUES (@Username, @Password, @Role)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", userDto.Username);
                    command.Parameters.AddWithValue("@Password", userDto.Password); // Для безпеки зазвичай зберігаємо хеш паролю
                    command.Parameters.AddWithValue("@Role", userDto.Role);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Update(UserDto userDto)  // Реалізація методу Update
        {
            using (SqlConnection connection = new SqlConnection(DBConfiguration.ConnectionString))
            {
                connection.Open();
                string query = "UPDATE Users SET Username = @Username, Password = @Password, Role = @Role WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", userDto.Id);
                    command.Parameters.AddWithValue("@Username", userDto.Username);
                    command.Parameters.AddWithValue("@Password", userDto.Password);  // Знову ж, хеш паролю замість простого тексту
                    command.Parameters.AddWithValue("@Role", userDto.Role);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(UserDto userDto)  // Реалізація методу Delete
        {
            using (SqlConnection connection = new SqlConnection(DBConfiguration.ConnectionString))
            {
                connection.Open();
                string query = "DELETE FROM Users WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", userDto.Id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public UserDto GetById(int userId)  // Реалізація методу GetById
        {
            UserDto user = null;
            using (SqlConnection connection = new SqlConnection(DBConfiguration.ConnectionString))
            {
                connection.Open();
                string query = "SELECT Id, Username, Role FROM Users WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", userId);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        user = new UserDto
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Username = reader["Username"].ToString(),
                            Role = reader["Role"].ToString()
                        };
                    }
                }
            }
            return user;
        }

        public IEnumerable<UserDto> GetAll()  // Заміна List на IEnumerable
        {
            List<UserDto> users = new List<UserDto>();
            using (SqlConnection connection = new SqlConnection(DBConfiguration.ConnectionString))
            {
                connection.Open();
                string query = "SELECT Id, Username, Role FROM Users";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        users.Add(new UserDto
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Username = reader["Username"].ToString(),
                            Role = reader["Role"].ToString()
                        });
                    }
                }
            }
            return users;  // Повертається IEnumerable
        }

    }
}
