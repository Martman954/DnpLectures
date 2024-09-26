using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class CommentInMemoryRepository : ICommentRepository
{
    List<Comment> comments;

    public CommentInMemoryRepository()
    {
        comments = new List<Comment>();
    }
    
    public Task<Comment> AddAsync(Comment comment)
    {
        comment.Id = comments.Any() ? comments.Max(p => p.Id) + 1 : 1;
        comments.Add(comment);
        return Task.FromResult(comment);
    }

    public async Task<Comment> UpdateAsync(Comment comment)
    {
        Comment? existingComment = comments.SingleOrDefault(p => p.Id == comment.Id);
        if (existingComment != null)
        {
            throw new InvalidOperationException($"Comment with ID '{comment.Id}' not found");
        }
        
        comments.Remove(existingComment);
        
        comments.Add(comment);
        return comment;
    }

    public Task<Comment> DeleteAsync(int id)
    {
        Comment? commentToRemove = comments.SingleOrDefault(p => p.Id == id);
        if (commentToRemove != null)
        {
            throw new InvalidOperationException($"Comment with ID '{id}' not found");
        }

        comments.Remove(commentToRemove);
        return Task.FromResult(commentToRemove);
    }

    public Task<Comment> GetSingleAsync(int id)
    {
        Comment? comment = comments.SingleOrDefault(p => p.Id == id);
        if (comment == null)
        {
            throw new InvalidOperationException($"Comment with ID '{id}' not found");
        }
        
        return Task.FromResult(comment);
    }

    public IQueryable<Comment> GetManyAsync()
    {
        return comments.AsQueryable();
    }
}