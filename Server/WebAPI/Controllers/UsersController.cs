﻿using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;
using ApiContracts;
using Entities;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(IUserRepository userRepository) : ControllerBase {
    
    
    [HttpPost]
    public async Task<ActionResult<UserDto>> AddUser([FromBody] CreateUserDto request) {
        User user = new User {
            Username = request.Username,
            Password = request.Password
        };
        
        
        User createdUser = await userRepository.AddAsync(user);
        UserDto userDto = new() {
            Id = createdUser.Id,
            Username = createdUser.Username
        };
        return Created($"/Users/{userDto.Id}", createdUser); 
    }
    
    [HttpGet]
    public ActionResult<IEnumerable<UserDto>> GetUsers([FromQuery] string? username) {
        IEnumerable<User> users = userRepository.GetManyAsync();
        
        if (username != null) {
            users = users.Where(user => user.Username.Contains(username));
        }
        IEnumerable<UserDto> userDtos = users.Select(user => new UserDto {
            Id = user.Id,
            Username = user.Username
        });
        return Ok(userDtos);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetUser(int id) {
        User user = await userRepository.GetSingleAsync(id);
        UserDto userDto = new() {
            Id = user.Id,
            Username = user.Username
        };
        return Ok(userDto);
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUser(int id) {
        await userRepository.DeleteAsync(id);
        return NoContent();
    }
    
    
}