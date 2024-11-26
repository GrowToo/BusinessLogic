using DTO;
using System.Collections.Generic;

namespace BusinessLogic.Interface
{
    public interface IUserManager
    {
        void AddUser(UserDto user);
        void UpdateUser(UserDto user);
        void DeleteUser(int userId);
        UserDto GetUserById(int userId);
        IEnumerable<UserDto> GetAllUsers();
        UserDto AuthenticateUser(string username, string password);
    }
}
