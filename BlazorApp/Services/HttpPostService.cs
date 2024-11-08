using System.Collections;
using System.Text.Json;
using ApiContracts;

namespace BlazorApp.Services;

public class HttpPostService : IPostService
{
    private readonly HttpClient client;

    public HttpPostService(HttpClient client)
    {
        this.client = client;
    }
    
    public async Task<PostDto?> AddPostAsync(CreatePostDto request)
    {
        var response = client.PostAsJsonAsync("Posts", request).Result;
        response.EnsureSuccessStatusCode(); 
        return await response.Content.ReadFromJsonAsync<PostDto>();
    }

    public async Task<PostDto?> GetPostAsync(int id)
    {
        return await client.GetFromJsonAsync<PostDto>($"Posts/{id}");
    }

    public async Task<ICollection<PostDto>?> GetPosts(string title)
    {
        HttpResponseMessage response = await client.GetAsync($"Posts?title={title}");
        string content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error: (" + response.StatusCode + "), ( " + response.Content + ")");
        }
        ICollection<PostDto>? posts = JsonSerializer.Deserialize<ICollection<PostDto>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        return posts;
    }


    public async Task DeletePostAsync(int id)
    {
        var response = await client.DeleteAsync($"Posts/{id}");  // Endpoint: /User/{id}
        response.EnsureSuccessStatusCode();
    }
}