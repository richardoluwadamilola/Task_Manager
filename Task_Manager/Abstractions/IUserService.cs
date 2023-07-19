using TaskManager.EntityDTOs;

namespace TaskManager.Abstractions
{
    public interface IUserService
    {
        UserDto CreateUser(UserDto userDto);
        UserDto GetUserById(int userId);
        UserDto GetUserByUsername(string username);
        IEnumerable<UserDto> GetAllUsers();
        UserDto UpdateUser(int userId, UserDto userDto);
        UserDto DeleteUser(int userId);
    }
}
