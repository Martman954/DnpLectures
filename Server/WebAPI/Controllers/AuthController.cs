using ApiContracts;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController(IUserRepository userRepository) : ControllerBase
{
    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> login([FromBody] CreateUserDto loginRequest)
    {
        User user = new User()
        {
            Username = loginRequest.Username,
            Password = loginRequest.Password
        };
        IEnumerable<User> existingUsers = userRepository.GetManyAsync();
        User authUser = existingUsers.FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password);
        // If user is not in the database
        if (authUser == null)
        {
            return Unauthorized();
        }
        
        
        UserDto userDto = new() {
            Id = authUser.Id,
            Username = authUser.Username
        };
        return userDto;
    }
}