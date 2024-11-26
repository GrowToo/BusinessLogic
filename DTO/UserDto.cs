namespace DTO
{
    public class UserDto
    {
        public int Id { get; set; }            // Унікальний ідентифікатор користувача
        public string Username { get; set; }   // Ім'я користувача
        public string Password { get; set; }   // Пароль
        public string Role { get; set; }       // Роль користувача (наприклад, "Приймальник товару")
    }
}
