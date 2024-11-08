using ApiContracts;

namespace BlazorApp.Services;

public interface IUserService
{
    public Task<UserDto?> AdduserAsync(CreateUserDto request);
    public Task<UserDto?> GetUserAsync(int id);
    public Task<ICollection<UserDto>?> GetUsers(string username);
    public Task DeleteUserAsync(int id);
}