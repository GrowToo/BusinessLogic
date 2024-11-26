using System.Collections.Generic;
using DAL.Concrete;
using DTO;

namespace DAL.Interface
{
    public interface IUserDal
    {
        void Add(UserDto userDto);  // Додати користувача
        void Update(UserDto userDto); // Оновити користувача
        void Delete(UserDto userDto);  // Видалити користувача
        UserDto GetById(int userId);  // Отримати користувача за ID
        IEnumerable<UserDto> GetAll();  // Отримати всіх користувачів
    }

}
