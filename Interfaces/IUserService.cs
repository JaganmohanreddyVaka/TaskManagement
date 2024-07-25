using TaskManagementSystem.Models;

namespace TaskManagementSystem.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto> GetUserByIdAsync(int userId);
        Task<UserDto> CreateUserAsync(UserDto userDto);
        Task<bool> UpdateUserAsync(int userId, UserDto userDto);
        Task<bool> DeleteUserAsync(int userId);
    }
}
