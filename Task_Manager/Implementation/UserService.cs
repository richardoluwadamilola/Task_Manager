using System.Threading.Tasks;
using Task_Manager.Data;
using Task_Manager.Models;
using TaskManager.Abstractions;
using TaskManager.EntityDTOs;

namespace TaskManager.Implementation
{
    public class UserService : IUserService
    {
        private readonly TaskManagerDBContext _context;
        public UserService(TaskManagerDBContext context)
        {
            _context = context;
        }

        public UserDto CreateUser(UserDto userDto)
        {
            var user = new User
            {
                Username = userDto.Username,
            };

            var createdUser = _context.Add(user);
            _context.SaveChanges();

            return userDto;
        }

        public UserDto GetUserById(int userId)
        {
            // Implement the logic to retrieve a user by ID
            // Example:
            var user = _context.Users.Find(userId);

            if (user != null)
            {
                return new UserDto
                {
                    Id = user.Id,
                    Username = user.Username,
                    // Set other properties as needed
                };
            }

            return null;
        }

        public UserDto GetUserByUsername(string username)
        {
            // Implement the logic to retrieve a user by username
            // Example:
            var user = _context.Users.FirstOrDefault(u => u.Username == username);


            if (user != null)
            {
                return new UserDto
                {
                    Id = user.Id,
                    Username = user.Username,
                    // Set other properties as needed
                };
            }

            return null;
        }
        public IEnumerable<UserDto> GetAllUsers()
        {
            // Implement the logic to retrieve all users
            // Example:
            var users = _context.Users.ToList();

            var userDtos = new List<UserDto>();

            foreach (var user in users)
            {
                userDtos.Add(new UserDto
                {
                    Id = user.Id,
                    Username = user.Username,
                    // Set other properties as needed
                });
            }

            return userDtos;
        }

        public UserDto UpdateUser(int userId, UserDto userDto)
        {
            // Implement the logic to update a user
            // Example:
            var existingUser = _context.Users.Find(userId);

            if (existingUser != null)
            {
                existingUser.Username = userDto.Username;
                // Update other properties as needed
                _context.SaveChanges();

                return userDto;
            }

            return null;
        }

        public UserDto DeleteUser(int userId)
        {
            // Implement the logic to delete a user
            // Example:
            var existingUser = _context.Users.Find(userId);

            if (existingUser != null)
            {
                _context.Users.Remove(existingUser);
                _context.SaveChanges();

                //return userId;
               
            }

            return null;
        }

    }
}
