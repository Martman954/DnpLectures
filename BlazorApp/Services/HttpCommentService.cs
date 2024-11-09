using System.Text.Json;
using ApiContracts;

namespace BlazorApp.Services;

public class HttpCommentService : ICommentService
{
    private readonly HttpClient client;

    public HttpCommentService(HttpClient client)
    {
        this.client = client;
    }
    public async Task<CommentDto?> AddCommentAsync(CreateCommentDto request)
    {
        var response = client.PostAsJsonAsync("Comments", request).Result;
        response.EnsureSuccessStatusCode(); 
        return await response.Content.ReadFromJsonAsync<CommentDto>();
    }

    public async Task<CommentDto?> GetCommentAsync(int id)
    {
        return await client.GetFromJsonAsync<CommentDto>($"Comments/{id}");
    }

    public async Task<ICollection<CommentDto>?> GetComments(int postId)
    {
        HttpResponseMessage response = await client.GetAsync($"Comments?postId={postId}");
        string content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error: (" + response.StatusCode + "), ( " + response.Content + ")");
        }
        ICollection<CommentDto>? comments = JsonSerializer.Deserialize<ICollection<CommentDto>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        return comments;
    }

    public async Task DeleteCommentAsync(int id)
    {
        var response = await client.DeleteAsync($"Comments/{id}");  
        response.EnsureSuccessStatusCode();
    }
}