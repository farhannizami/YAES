using UserService.DTOs;

namespace UserService.Interfaces
{
    public interface IUserService
    {
        Task<UserResponse> CreateUserAsync(CreateUserRequest request);
        Task<UserResponse?> GetUserByIdAsync(int Id);
        Task<List<UserResponse>> GetAllUsersAsync();
    }
}
