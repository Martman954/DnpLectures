using System.Collections;
using System.Text.Json;
using ApiContracts;

namespace BlazorApp.Services;

public class HttpUserService : IUserService
{
    private readonly HttpClient client;

    public HttpUserService(HttpClient client)
    {
        this.client = client;
    }
    public async Task<UserDto?> AdduserAsync(CreateUserDto request)
    {
        var response = await client.PostAsJsonAsync("User", request);
        response.EnsureSuccessStatusCode(); // Throw if not a success code
        return await response.Content.ReadFromJsonAsync<UserDto>();
    }

    public async Task<UserDto?> GetUserAsync(int id)
    {
        return await client.GetFromJsonAsync<UserDto>($"User/{id}");
    }

    

    public async Task<ICollection<UserDto>?> GetUsers(string username)
    {
        HttpResponseMessage response = await client.GetAsync($"User?username={username}");
        string content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error: (" + response.StatusCode + "), ( " + response.Content + ")");
        }
        
        ICollection<UserDto>? users = JsonSerializer.Deserialize<ICollection<UserDto>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        return users;
    }

    public async Task DeleteUserAsync(int id)
    {
        var response = await client.DeleteAsync($"User/{id}");  // Endpoint: /User/{id}
        response.EnsureSuccessStatusCode();
    }
}