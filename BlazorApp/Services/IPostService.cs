using ApiContracts;

namespace BlazorApp.Services;

public interface IPostService
{
    public Task<PostDto?> AddPostAsync(CreatePostDto request);
    public Task<PostDto?> GetPostAsync(int id);
    public Task<ICollection<PostDto>?> GetPosts(string title);
    public Task DeletePostAsync(int id);
}