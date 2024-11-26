using BusinessLogic.Interface;
using DAL.Interface;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Concrete
{
    public class UserManager : IUserManager
    {
        private readonly IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public void AddUser(UserDto user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            // Створення нового користувача в базі даних через DAL
            _userDal.Add(user); // Додати безпосередньо UserDto
        }

        public void UpdateUser(UserDto user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var existingUser = _userDal.GetById(user.Id);
            if (existingUser == null) throw new InvalidOperationException("User not found");

            // Оновлюємо інформацію про користувача
            existingUser.Username = user.Username;
            existingUser.Password = user.Password; // У реальній ситуації пароль має бути захищений
            existingUser.Role = user.Role;

            _userDal.Update(existingUser); // Оновити через DAL
        }

        public void DeleteUser(int userId)
        {
            var user = _userDal.GetById(userId);
            if (user == null) throw new InvalidOperationException("User not found");

            _userDal.Delete(user); // Видалити користувача
        }

        public UserDto GetUserById(int userId)
        {
            var user = _userDal.GetById(userId);
            if (user == null) throw new InvalidOperationException("User not found");

            return user; // Повернути UserDto
        }

        public IEnumerable<UserDto> GetAllUsers()
        {
            var users = _userDal.GetAll();
            return users; // Повернути список UserDto
        }

        public UserDto AuthenticateUser(string username, string password)
        {
            var user = _userDal.GetAll().FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase) && u.Password == password);
            if (user == null)
                return null; // Якщо користувач не знайдений

            return user; // Повертаємо знайдений UserDto
        }
    }
}
