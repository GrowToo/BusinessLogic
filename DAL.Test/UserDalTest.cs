/*using DAL.Concrete;
using DTO;
using NUnit.Framework;
using System;
using Microsoft.Data.SqlClient;
using System.Data.SqlClient;
using System.Linq;

[TestFixture]
public class UserTests
{
    private string _connectionString = "ItemReceiver";
    private UserDal _userDal;

    [SetUp]
    public void SetUp()
    {
        // Ініціалізація класу для тестів
        _userDal = new UserDal(_connectionString);

        // Очищення таблиці перед кожним тестом, щоб уникнути впливу попередніх даних
        ClearUsersTable();
    }

    // Тест для додавання користувача
    [Test]
    public void AddUser_ShouldAddUserSuccessfully()
    {
        // Arrange
        var newUser = new UserDto { Username = "testuser", Password = "testpassword", Role = "admin" };

        // Act
        _userDal.Add(newUser);

        // Assert
        var addedUser = _userDal.GetById(newUser.Id);
        Assert.IsNotNull(addedUser);
        Assert.AreEqual(newUser.Username, addedUser.Username);
        Assert.AreEqual(newUser.Role, addedUser.Role);
    }

    // Тест для отримання всіх користувачів
    [Test]
    public void GetUsers_ShouldReturnUsers()
    {
        // Arrange
        _userDal.Add(new UserDto { Username = "testuser1", Password = "password1", Role = "admin" });
        _userDal.Add(new UserDto { Username = "testuser2", Password = "password2", Role = "user" });

        // Act
        var users = _userDal.GetUsers();

        // Assert
        Assert.IsNotNull(users);
        Assert.AreEqual(2, users.Count);
    }

    // Тест для отримання користувача за ID
    [Test]
    public void GetUserById_ShouldReturnUser()
    {
        // Arrange
        var user = new UserDto { Username = "testuser", Password = "password", Role = "user" };
        _userDal.Add(user);

        // Act
        var fetchedUser = _userDal.GetById(user.Id);

        // Assert
        Assert.IsNotNull(fetchedUser);
        Assert.AreEqual(user.Username, fetchedUser.Username);
        Assert.AreEqual(user.Role, fetchedUser.Role);
    }

    // Тест для оновлення користувача
    [Test]
    public void UpdateUser_ShouldUpdateUserSuccessfully()
    {
        // Arrange
        var user = new UserDto { Username = "olduser", Password = "oldpassword", Role = "user" };
        _userDal.Add(user);

        user.Username = "updateduser";
        user.Password = "updatedpassword";
        user.Role = "admin";

        // Act
        _userDal.Update(user);

        // Assert
        var updatedUser = _userDal.GetById(user.Id);
        Assert.AreEqual("updateduser", updatedUser.Username);
        Assert.AreEqual("admin", updatedUser.Role);
    }

    // Тест для видалення користувача
    [Test]
    public void DeleteUser_ShouldDeleteUserSuccessfully()
    {
        // Arrange
        var user = new UserDto { Username = "userToDelete", Password = "password", Role = "user" };
        _userDal.Add(user);

        // Act
        _userDal.Delete(user);

        // Assert
        var deletedUser = _userDal.GetById(user.Id);
        Assert.IsNull(deletedUser);
    }

    // Тест для отримання всіх користувачів через GetAll
    [Test]
    public void GetAllUsers_ShouldReturnAllUsers()
    {
        // Arrange
        _userDal.Add(new UserDto { Username = "user1", Password = "password1", Role = "admin" });
        _userDal.Add(new UserDto { Username = "user2", Password = "password2", Role = "user" });

        // Act
        var allUsers = _userDal.GetAll();

        // Assert
        Assert.IsNotNull(allUsers);
        Assert.AreEqual(2, allUsers.Count());
    }

    // Очистка таблиці Users після кожного тесту
    private void ClearUsersTable()
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            var query = "DELETE FROM Users";
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
        ClearUsersTable();
    }
}
*/