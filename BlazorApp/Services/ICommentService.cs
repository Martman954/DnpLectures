using ApiContracts;

namespace BlazorApp.Services;

public interface ICommentService
{
    public Task<CommentDto?> AddCommentAsync(CreateCommentDto request);
    public Task<CommentDto?> GetCommentAsync(int id);
    public Task<ICollection<CommentDto>?> GetComments(int postId);
    public Task DeleteCommentAsync(int id);
}